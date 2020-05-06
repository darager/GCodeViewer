using System.ComponentModel;
using System.Windows.Media;

namespace GCodeViewer.WPF.Controls
{
    public class NumericUpDownViewModel : INotifyPropertyChanged
    {
        public string Text
        {
            get => _text;
            set
            {
                if (value == _text) return;

                _text = value;
                OnPropertyChanged("Value");
            }
        }
        private string _text;

        public float MinValue
        {
            get => _minValue;
            set
            {
                if (value == _minValue) return;

                _minValue = value;
                OnPropertyChanged("MinValue");
            }
        }
        private float _minValue;

        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if (value == _maxValue) return;

                _maxValue = value;
                OnPropertyChanged("MaxValue");
            }
        }
        private float _maxValue;

        public float StepSize
        {
            get => _stepSize;
            set
            {
                if (value == _stepSize) return;

                _stepSize = value;
                OnPropertyChanged("StepSize");
            }
        }
        private float _stepSize;

        public Brush Foreground
        {
            get => _foreground;
            set
            {
                if (value == _foreground) return;

                _foreground = value;
                OnPropertyChanged("Foreground");
            }
        }
        private Brush _foreground;

        public Brush Background
        {
            get => _background;
            set
            {
                if (value == _background) return;

                _background = value;
                OnPropertyChanged("Background");
            }
        }
        private Brush _background;

        public NumericUpDownViewModel()
        {
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
