using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using GCodeViewer.Library;
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

        internal GCodeTextEditor TextEditor { get; set; }

        #region required by Ninject

        public Type Type => typeof(TextEditorPageViewModel);

        public object Create(IContext context) => this;

        #endregion

        private PageNavigationService _pageNavigationService;
        private SettingsPageViewModel _settingsViewModel;

        private string? _filePath;

        public TextEditorPageViewModel(PageNavigationService pageNavigationService, SettingsPageViewModel settingsViewModel)
        {
            _pageNavigationService = pageNavigationService;
            _settingsViewModel = settingsViewModel;

            InitializeCommands();
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
