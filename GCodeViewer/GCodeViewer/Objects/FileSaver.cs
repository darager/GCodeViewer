﻿using System.IO;
using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using GCodeViewer.Interfaces.FileAccess.FileChooser;

namespace GCodeViewer.Objects
{
    public class FileSaver : IFileSaver
    {
        public ITextViewModel TextViewModel { get; set; }
        public IFileChooser FileChooser { get; set; }

        public FileSaver(ITextViewModel textviewModel, IFileChooser fileChooser)
        {
            this.TextViewModel = textviewModel;
            this.FileChooser = fileChooser;
        }

        public void WriteToFile()
        {
            string[] content = TextViewModel.GetCurrentContent();
            IFile file = FileChooser.GetFile();

            using (var stream = new FileStream(file.FilePath, FileMode.OpenOrCreate))
                using (var writer = new StreamWriter(stream))
                    foreach (string line in content)
                        writer.WriteLineAsync(line);
        }
    }
}