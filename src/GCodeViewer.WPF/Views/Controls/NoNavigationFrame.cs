using System.Windows.Controls;
using System.Windows.Input;

namespace GCodeViewer.WPF.Views.Controls
{
    public class NoNavigationFrame : Frame
    {
        public NoNavigationFrame()
        {
            foreach (var vNavigationCommand in new RoutedUICommand[]
                {
                    NavigationCommands.BrowseBack,
                    NavigationCommands.BrowseForward,
                    NavigationCommands.BrowseHome,
                    NavigationCommands.BrowseStop,
                    NavigationCommands.Refresh,
                    NavigationCommands.Favorites,
                    NavigationCommands.Search,
                    NavigationCommands.IncreaseZoom,
                    NavigationCommands.DecreaseZoom,
                    NavigationCommands.Zoom,
                    NavigationCommands.NextPage,
                    NavigationCommands.PreviousPage,
                    NavigationCommands.FirstPage,
                    NavigationCommands.LastPage,
                    NavigationCommands.GoToPage,
                    NavigationCommands.NavigateJournal })
            {
                this.CommandBindings.Add(new CommandBinding(vNavigationCommand, (sender, args) => { }));
            }
        }
    }
}
