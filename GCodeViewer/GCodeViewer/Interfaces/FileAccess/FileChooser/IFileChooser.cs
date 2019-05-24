﻿using System;

namespace GCodeViewer.Interfaces.FileAccess.FileChooser
{
    public interface IFileChooser
    {
        IFile File { get; }

        IFile GetFile();
        void SwapFile(IFile file);

        event FileSwappedEventHandler FileSwapped;
    }

    public delegate void FileSwappedEventHandler(object sender, FileSwappedEventArgs e);
    public class FileSwappedEventArgs : EventArgs
    {
        public FileSwappedEventArgs(IFile file)
        {
            File = file;
        }

        public IFile File { get; set; }
    }
}
