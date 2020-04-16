using System.IO;

namespace GCodeViewer.WPF.Abstractions.FileAccess
{
    public interface IFile
    {
        FileStream GetFileStream();
    }
}
