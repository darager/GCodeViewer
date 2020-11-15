namespace GCodeViewer.Library.PrinterSettings
{
    public class CAxisParserInfo
    {
        public string GCodePattern { get; set; } = "C{{value}}";

        public float ValueAt360Degrees { get; set; } = 360;
    }
}
