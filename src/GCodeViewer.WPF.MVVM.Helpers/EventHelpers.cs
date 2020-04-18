using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.WPF.MVVM.Helpers
{
    public static class EventHelpers
    {
        private static Dictionary<Action, System.Timers.Timer> _timers = new Dictionary<Action, System.Timers.Timer>();

        public static void DebounceAction(Action action, int delay = 300)
        {
            action();
        }
    }
}
