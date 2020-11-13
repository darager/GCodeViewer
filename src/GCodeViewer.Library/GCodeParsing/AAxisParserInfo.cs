namespace GCodeViewer.Library.PrinterSettings
{
    public class AAxisParserInfo
    {
        public string GCodePattern { get; set; } = "A {{value}}";

        public float MinValueAAxis { get; set; } = 0;
        public float MaxValueAAxis { get; set; } = 0;

        public float MinDegreesAAxis { get; set; } = 0;
        public float MaxDegreesAAxis { get; set; } = 0;
    }
}
