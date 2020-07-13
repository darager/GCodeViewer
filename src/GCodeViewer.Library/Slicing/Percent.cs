namespace GCodeViewer.Library.Slicing
{
    public class Percent
    {
        public float Value { get; private set; }

        public Percent(float zeroToOne)
        {
            Value = zeroToOne;
        }
    }
}
