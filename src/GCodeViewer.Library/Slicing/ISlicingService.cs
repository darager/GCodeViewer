﻿namespace GCodeViewer.Library.Slicing
{
    public interface ISlicingService
    {
        public string Slice(string stlFilePath, string ConfigFile, SlicingOptions options);
    }
}