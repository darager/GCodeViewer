using System;
using System.ComponentModel;
using System.Windows.Input;
using GCodeViewer.WPF.Controls.TextEditor;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.Settings;
using Ninject.Activation;

namespace GCodeViewer.WPF.TextEditor
{
    public class TextEditorPageViewModel : INotifyPropertyChanged, ITextEditor, IProvider
    {
        public ICommand OpenOtherFile { get; private set; }
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

        public TextEditorPageViewModel(PageNavigationService pageNavigationService, SettingsPageViewModel settingsViewModel)
        {
            _pageNavigationService = pageNavigationService;
            _settingsViewModel = settingsViewModel;

            HandleTextChanged = new RelayCommand((_) => TextChanged?.Invoke(this, EventArgs.Empty));
            GoToSettingsPage = new RelayCommand((_) =>
            {
                _settingsViewModel.ShowAAxisOffset();
                _pageNavigationService.GoTo(Navigation.Navigation.SettingsPage);
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
