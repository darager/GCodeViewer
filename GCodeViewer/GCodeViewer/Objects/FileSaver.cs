using System.IO;
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

        public void SaveCurrentFile()
        {
            string[] content = TextViewModel.GetCurrentContent();
            IFile file = FileChooser.GetFile();

            SaveFile(file, content);
        }
        public void SaveToFile(IFile file)
        {
            string[] content = TextViewModel.GetCurrentContent();

            SaveFile(file, content);
        }

        void SaveFile(IFile file, string[] content)
        {
            using (var stream = file.GetFileStream())
            {
                using (var writer = new StreamWriter(stream))
                    foreach (string line in content)
                        writer.WriteLine(line);
            }
        }
    }
}
