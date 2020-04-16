using GCodeViewer.WPF.Abstractions.FileAccess;
using System.Collections;
using System.IO;

namespace GCodeViewer.WPF.Components.FileAccess
{
    public class TextBuffer : ITextBuffer
    {
        public IFileChooser FileChooser { get; set; }
        private string[] fileContent;

        public TextBuffer(IFileChooser fileChooser)
        {
            FileChooser = fileChooser;
            fileContent = new string[0];
        }

        public string[] GetContent()
        {
            return fileContent;
        }
        public void LoadFileContent()
        {
            IFile file = FileChooser.GetFile();
            FileStream stream = file.GetFileStream();
            var reader = new StreamReader(stream);

            fileContent = LoadFileContent(reader);

            reader.Close();
            stream.Close();
        }

        private string[] LoadFileContent(StreamReader reader)
        {
            var content = new ArrayList();
            string line;

            while ((line = reader.ReadLine()) != null)
                content.Add(line);

            return (string[])content.ToArray(typeof(string));
        }
    }
}
