using GCodeViewer.WPF.Abstractions.FileAccess;
using GCodeViewer.WPF.Abstractions.ViewModels;
using System.IO;

namespace GCodeViewer.WPF.Components.FileAccess
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

        public void SaveCurrentFile()
        {
            string[] content = GetCurrentContent();
            IFile file = FileChooser.GetFile();

            SaveFile(file, content);
        }
        public void SaveToFile(IFile file)
        {
            string[] content = GetCurrentContent();
            SaveFile(file, content);
        }

        private void SaveFile(IFile file, string[] content)
        {
            using (var stream = file.GetFileStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (string line in content)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }
        private string[] GetCurrentContent()
        {
            return TextViewModel.GetCurrentContent();
        }
    }
}
