namespace GCodeViewer.Library.Slicing
{
    public class SlicingOptions
    {
        public Percent Infill { get; set; }
        public float LayerHeight { get; set; }

        public bool HasBrimOrSkirt { get; set; }
    }
}
