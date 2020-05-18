﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using GCodeViewer.WPF.MVVM.Helpers;
using GCodeViewer.WPF.Navigation;
using GCodeViewer.WPF.TextEditor;
using Microsoft.Win32;

namespace GCodeViewer.WPF.Starting
{
    public class StartingPageViewModel : INotifyPropertyChanged
    {
        public ICommand GoToSettingsPage { get; private set; }
        public ICommand StartSlicingWorkflow { get; private set; }
        public ICommand OpenGCodeFile { get; private set; }

        private readonly ITextEditor _texteditor;
        private readonly PageNavigationService _navigationService;

        public StartingPageViewModel(PageNavigationService navigationService, TextEditorPageViewModel texteditor)
        {
            _navigationService = navigationService;
            _texteditor = texteditor;

            OpenGCodeFile = new RelayCommand(LoadGcodeFile);
            StartSlicingWorkflow = new RelayCommand(LoadSTLFile);
            GoToSettingsPage = new RelayCommand((_) => navigationService.GoTo(Navigation.Navigation.SettingsPage));
        }

        private async void LoadGcodeFile(object _)
        {
            var ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.Filter = "gcode files (*.gcode)|*.gcode";
            ofd.FilterIndex = 2;

            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;

                using var reader = File.OpenText(filePath);
                string content = await reader.ReadToEndAsync();

                _texteditor.SetText(content);
                _navigationService.GoTo(Navigation.Navigation.GCodePreviewPage);
            }
        }
        private void LoadSTLFile(object _)
        {
            // TODO:
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}