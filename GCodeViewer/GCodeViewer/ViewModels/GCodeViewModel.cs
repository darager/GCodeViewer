using GCodeViewer.Interfaces.FileAccess;
using GCodeViewer.Interfaces.ViewModels;
using System;

namespace GCodeViewer.ViewModels
{
    public class GCodeViewModel : ITextViewModel
    {
        public IFileBuffer FileBuffer { get; set; }

        public GCodeViewModel(IFileBuffer fileBuffer)
        {
            this.FileBuffer = fileBuffer;
        }



        public void ChangeLine(int lineIndex, string content)
        {
            throw new NotImplementedException();
        }
        public void RestoreOriginalContent()
        {
            throw new NotImplementedException();
        }
        public string[] GetCurrentContent()
        {
            throw new NotImplementedException();
        }
    }
}
