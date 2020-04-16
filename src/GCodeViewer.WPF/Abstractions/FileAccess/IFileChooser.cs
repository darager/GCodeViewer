﻿namespace GCodeViewer.WPF.Abstractions.FileAccess
{
    public interface IFileChooser
    {
        IFile GetFile();
        void SwapFile(IFile file);
    }
}
