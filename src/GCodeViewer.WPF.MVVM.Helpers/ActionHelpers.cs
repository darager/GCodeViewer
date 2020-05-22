using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace GCodeViewer.WPF.MVVM.Helpers
{
    public static class ActionHelpers
    {
        private static Dictionary<Action, DateTime> _throttleTimes = new Dictionary<Action, DateTime>();

        public static Action Throttle(this Action action, int maxTimesPerSecond)
        {
            if (!_throttleTimes.ContainsKey(action))
            {
                _throttleTimes.Add(action, DateTime.UtcNow);
            }

            var lastCall = _throttleTimes[action];
            var passedTime = DateTime.UtcNow - lastCall;

            int minDelay = 1000 / maxTimesPerSecond;
            if (passedTime.Milliseconds > minDelay)
            {
                _throttleTimes[action] = DateTime.UtcNow;
                action();
            }

            return action;
        }

        private static Dictionary<Action, Timer> _waitForTimers = new Dictionary<Action, Timer>();

        public static Action WaitFor(this Action action, int debounceTimeInMillis)
        {
            if (!_waitForTimers.ContainsKey(action))
            {
                var dispatcher = Dispatcher.CurrentDispatcher;

                var timer = new Timer();
                timer.Interval = debounceTimeInMillis;
                timer.AutoReset = false;
                timer.Elapsed += (s, e) => dispatcher.Invoke(action);
                timer.Start();

                return action;
            }

            _waitForTimers[action].Stop();
            _waitForTimers[action].Start();
            return action;
        }
    }
}
