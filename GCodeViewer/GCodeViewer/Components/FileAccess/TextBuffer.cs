using GCodeViewer.Abstractions.FileAccess;
using System.Collections;
using System.IO;

namespace GCodeViewer.Components.FileAccess
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
            using (FileStream stream = file.GetFileStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var content = new ArrayList();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        content.Add(line);
                    }

                    fileContent = (string[])content.ToArray(typeof(string));
                }
            }
        }
    }
}
