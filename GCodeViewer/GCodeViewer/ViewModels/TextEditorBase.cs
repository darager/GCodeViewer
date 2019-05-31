using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using System;

namespace GCodeViewer.ViewModels
{
    public class TextEditorBase : ITextViewModel
    {
        public ITextBuffer FileBuffer { get; set; }
        public string[] FileContent { get; set; }

        public TextEditorBase(ITextBuffer fileBuffer)
        {
            FileBuffer = fileBuffer;
        }

        public void ChangeLine(int lineIndex, string content)
        {
            throw new NotImplementedException();
        }
        public string[] GetCurrentContent()
        {
            return FileContent;
        }
        public void LoadBufferContent()
        {
            FileContent = FileBuffer.GetContent();
        }
        public bool IsCurrentStateSaved()
        {
            // TODO implement a flag that is set to true once the file is saved and set to false when some text changes.
            return true;
        }
        public bool IsFileLoaded()
        {
            return (FileContent != null);
        }
    }
}
