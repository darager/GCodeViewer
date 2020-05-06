using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.Controls
{
    public class NumericUpDownViewModel : INotifyPropertyChanged
    {
        public float Value
        {
            get;
            set;
        }

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
        private float _minValue = 0;

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
        private float _maxValue = 100;

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
        private float _stepSize = 10;

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
        private Brush _foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

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
        private Brush _background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        public ICommand DecreaseValue { get; private set; }
        public ICommand IncreaseValue { get; private set; }

        public NumericUpDownViewModel()
        {
            DecreaseValue = new RelayCommand((_) =>
            {
                float newValue = Value - StepSize;
                this.Value = (newValue < MaxValue) ? MaxValue : newValue;
            });
            IncreaseValue = new RelayCommand((_) =>
            {
                float newValue = Value + StepSize;
                this.Value = (newValue > MaxValue) ? MaxValue : newValue;
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
