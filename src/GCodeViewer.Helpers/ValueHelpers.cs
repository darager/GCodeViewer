namespace GCodeViewer.Helpers
{
    public static class ValueHelpers
    {
        public static float Constrain(this float @this, float min, float max)
        {
            float result = @this;

            if (@this >= max) result = max;
            else if (@this <= min) result = min;

            @this = result;
            return result;
        }
        public static float Scale(this float @this, float fromMin, float fromMax, float toMin, float toMax)
        {
            float scaledValue = toMin + (@this - fromMin) / (fromMax - fromMin) * (toMax - toMin);

            @this = scaledValue;
            return scaledValue;
        }
    }
}
