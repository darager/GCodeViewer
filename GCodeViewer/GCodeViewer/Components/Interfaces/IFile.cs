using System.IO;

namespace GCodeViewer.Interfaces.FileAccess
{
    public interface IFile
    {
        FileStream GetFileStream();
    }
}
