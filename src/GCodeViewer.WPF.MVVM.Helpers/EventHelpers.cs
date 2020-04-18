using System;
using System.Collections.Generic;

namespace GCodeViewer.WPF.MVVM.Helpers
{
    public static class EventHelpers
    {
        private static Dictionary<Action, System.Timers.Timer> _timers = new Dictionary<Action, System.Timers.Timer>();

        public static void DebounceAction(Action action, int delay = 300, bool returnToThread = true)
        {
            if (!_timers.ContainsKey(action))
            {
                var timer = new System.Timers.Timer
                {
                    AutoReset = false,
                    Interval = delay
                };
                timer.Elapsed += (s, e) => action();

                _timers.Add(action, timer);
                timer.Start();
            }
            else
            {
                _timers[action].Stop();
                _timers[action].Start();
            }
        }
    }
}
