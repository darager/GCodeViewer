using System.ComponentModel;
using System.Windows.Media;

namespace UserControlTest
{
    public class NumUpDownViewModel : INotifyPropertyChanged
    {
        public float NumValue
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;
                OnPropertyChanged("NumValue");
            }
        }
        private float _value = 10;

        public float MaxValue { get; } = 100;
        public float MinValue { get; } = -100;

        public SolidColorBrush Foreground { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public SolidColorBrush Background { get; } = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
