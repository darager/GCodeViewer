using System;
using System.Collections.Generic;

namespace GCodeViewer.WPF.MVVM.Helpers
{
    public static class ActionHelpers
    {
        private static Dictionary<Action, DateTime> _times = new Dictionary<Action, DateTime>();

        public static void Throttle(this Action action, int maxTimesPerSecond)
        {
            if (!_times.ContainsKey(action))
            {
                _times.Add(action, DateTime.UtcNow);
            }

            var lastCall = _times[action];
            var passedTime = DateTime.UtcNow - lastCall;

            int minDelay = 1000 / maxTimesPerSecond;
            if (passedTime.Milliseconds > minDelay)
            {
                _times[action] = DateTime.UtcNow;
                action();
            }
        }
    }
}
