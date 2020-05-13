namespace GCodeViewer.WPF.Controls.PointCloud.Helpers
{
    internal static class MiscHelpers
    {
        public static float Constrain(this float @this, float min, float max)
        {
            float result = @this;

            if (@this >= max) result = max;
            else if (@this <= min) result = min;

            @this = result;
            return result;
        }
    }
}
