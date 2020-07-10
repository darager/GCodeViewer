using System.ComponentModel;

namespace GCodeViewer.WPF.StlPositioning
{
    public class STLPositioningPageViewModel : INotifyPropertyChanged
    {
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
