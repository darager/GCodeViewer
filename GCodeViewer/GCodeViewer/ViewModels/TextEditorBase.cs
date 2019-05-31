using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsCurrentFileSaved()
        {
            // TODO: implement this
            return true;
        }
        public void LoadBufferContent()
        {
            FileContent = FileBuffer.GetContent();
        }
    }
}
