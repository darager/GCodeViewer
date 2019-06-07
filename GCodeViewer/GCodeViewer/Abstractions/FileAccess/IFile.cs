using System.IO;

namespace GCodeViewer.Abstractions.FileAccess
{
    public interface IFile
    {
        FileStream GetFileStream();
    }
}
