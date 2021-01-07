namespace GCodeViewer.Library.PrinterSettings
{
    public class AAxisParserInfo
    {
        public string GCodePattern { get; set; } = "T2 E{{value}} T0";

        public float MinValueAAxis { get; set; } = 0;
        public float MaxValueAAxis { get; set; } = 90;

        public float MinDegreesAAxis { get; set; } = 0;
        public float MaxDegreesAAxis { get; set; } = 90;
    }
}
