using System;

namespace GCodeViewer.Interfaces.FileAccess.FileChooser
{
    public interface IFileChooser
    {
        IFile GetFile();
        void SwapFile(IFile file);
    }
}
