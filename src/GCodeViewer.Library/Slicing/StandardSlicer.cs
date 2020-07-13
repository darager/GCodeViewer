namespace GCodeViewer.Library.Slicing
{
    public class StandardSlicer : ISlicingService
    {
        // TODO: make sure that for the  tilted prints no brim or skirt are printed
        public string Slice(string stlFilePath, string configFile, SlicingOptions options = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
