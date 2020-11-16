using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using GCodeViewer.Library.GCodeParsing;
using GCodeViewer.Library.PrinterSettings;
using GCodeViewer.Library.Renderables;
using GCodeViewer.Library.Renderables.Things;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Settings;
using Microsoft.Win32;
using Ninject.Activation;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorPageViewModel : INotifyPropertyChanged, ITextEditor, IProvider
    {
        public ICommand OpenFile { get; private set; }
        public ICommand SaveFile { get; private set; }
        public ICommand SaveFileAs { get; private set; }
        public ICommand CloseFile { get; private set; }
        public ICommand GoToSettingsPage { get; private set; }
        public ICommand HandleTextChanged { get; private set; }
        public ICommand PreviewPrintingPositions { get; private set; }

        public ObservableCollection<SyntaxHighlightingRule> SyntaxHighlightRules { get; set; }

        internal GCodeTextEditor TextEditor { get; set; }

        #region required by Ninject

        public Type Type => typeof(TextEditorPageViewModel);

        public object Create(IContext context) => this;

        #endregion

        private readonly PageNavigationService _pageNavigationService;
        private readonly SettingsPageViewModel _settingsViewModel;
        private readonly IViewerScene _printerScene;

        private string? _filePath;

        private PointCloud _pointcloud;
        private SettingsService _settings;
        private GCodeAxisValueExtractor _extractor = new GCodeAxisValueExtractor();

        public TextEditorPageViewModel(PageNavigationService pageNavigationService,
                                       SettingsPageViewModel settingsViewModel,
                                       IViewerScene printerScene,
                                       SettingsService settings)
        {
            _pageNavigationService = pageNavigationService;
            _settingsViewModel = settingsViewModel;
            _printerScene = printerScene;
            _settings = settings;

            InitializeCommands();
            InitializeSyntaxHighlighting();

            _settings.SettingsUpdated += (s, e) => UpdateACAxisSyntaxHighlighting();
        }

        private SyntaxHighlightingRule _aAxisHighlighting;
        private SyntaxHighlightingRule _cAxisHighlighting;
        private string _floatPattern = "\\d+(\\.\\d+)?";

        private void UpdateACAxisSyntaxHighlighting()
        {
            //TODO: update the syntaxhighlighting according to the gcodepatterns in the settings

            var red = Color.FromRgb(255, 0, 0);

            string aAxisPattern = _settings.Settings.AAxisParserInfo.GCodePattern
                                                        .Trim()
                                                        .Replace("{{value}}", _floatPattern);
            string cAxisPattern = _settings.Settings.CAxisParserInfo.GCodePattern
                                                        .Trim()
                                                        .Replace("{{value}}", _floatPattern);

            if (SyntaxHighlightRules.Contains(_aAxisHighlighting))
                SyntaxHighlightRules.Remove(_aAxisHighlighting);
            if (SyntaxHighlightRules.Contains(_cAxisHighlighting))
                SyntaxHighlightRules.Remove(_cAxisHighlighting);

            _aAxisHighlighting = new SyntaxHighlightingRule(aAxisPattern, red);
            _cAxisHighlighting = new SyntaxHighlightingRule(cAxisPattern, red);

            SyntaxHighlightRules.Add(_aAxisHighlighting);
            SyntaxHighlightRules.Add(_cAxisHighlighting);
        }

        private void InitializeSyntaxHighlighting()
        {
            SyntaxHighlightRules = new ObservableCollection<SyntaxHighlightingRule>();

            UpdateACAxisSyntaxHighlighting();
        }

        private void InitializeCommands()
        {
            HandleTextChanged = new RelayCommand((_) =>
            {
                TextChanged?.Invoke(this, EventArgs.Empty);
            });

            GoToSettingsPage = new RelayCommand((_) =>
            {
                _settingsViewModel.ShowAAxisOffset();
                _pageNavigationService.GoTo(Navigation.Navigation.SettingsPage);

                if (_pointcloud != null)
                    _printerScene.Remove(_pointcloud);
            });

            OpenFile = new RelayCommand((_) =>
            {
                var ofd = new OpenFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "gcode files (*.gcode)|*.gcode",
                    FilterIndex = 2
                };

                if (ofd.ShowDialog() == true)
                {
                    _filePath = ofd.FileName;
                    using var reader = File.OpenText(_filePath); ;
                    string text = reader.ReadToEnd();

                    this.SetText(text);
                }
            });

            SaveFile = new RelayCommand((_) =>
            {
                if (_filePath is null)
                {
                    SaveFileAs.Execute(parameter: null);
                    return;
                }

                var text = GetText();
                File.WriteAllText(_filePath, text);
            });

            SaveFileAs = new RelayCommand((_) =>
            {
                var sfd = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "gcode files (*.gcode)|*.gcode",
                    FilterIndex = 2
                };

                if (sfd.ShowDialog() != null)
                {
                    _filePath = sfd.FileName;
                    SaveFile.Execute(parameter: null);
                }
            });

            CloseFile = new RelayCommand((_) =>
            {
                _filePath = null;
                SetText("");

                if (_pointcloud != null)
                    _printerScene.Remove(_pointcloud);
            });

            PreviewPrintingPositions = new RelayCommand(_ =>
            {
                var gcodeLines = GetText().Split("\n");
                float aAxisOffset = _settings.Settings.PrinterDimensions.AAxisOffset;

                var points = _extractor.ExtractAxisValues(gcodeLines,
                                                          _settings.Settings.AAxisParserInfo,
                                                          _settings.Settings.CAxisParserInfo)
                                       .RemoveNonExtruding()
                                       .Select(a => a.GetEquivalentPoint(aAxisOffset));

                if (_pointcloud != null)
                    _printerScene.Remove(_pointcloud);

                var listpoints = points.ToList();
                _pointcloud = new PointCloud(points);
                _printerScene.Add(_pointcloud, new Point3D(0, 0, 0), (0, 0, 0));
            });
        }

        public void SetText(string text)
        {
            TextEditor.Text = text;
        }

        public string GetText()
        {
            return TextEditor.Text;
        }

        public event EventHandler TextChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
