namespace GCodeViewer.Library.PrinterSettings
{
    public class CAxisParserInfo
    {
        public string GCodePattern { get; set; } = "T3 E{{value}} T0";

        public float ValueAt360Degrees { get; set; } = 360;
    }
}
