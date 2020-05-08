using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GCodeViewer.WPF.MVVM.Helpers;

namespace GCodeViewer.WPF.Controls
{
    public class NumericUpDownViewModel : INotifyPropertyChanged
    {
        public float Value
        {
            get => _value;
            set
            {
                if (_value == value) return;

                _value = value;

                Text = _value.ToString();
            }
        }
        public float _value = 0;

        public float StepSize { get; set; } = 10;

        public float MinValue { get; set; } = 0;
        public float MaxValue { get; set; } = 100;

        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;

                if (value == "-")
                    return;

                _text = value;

                float newValue = float.Parse(Text);
                Value = EnsureValueConstraints(newValue);

                OnPropertyChanged("Text");
            }
        }

        private string _text;

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
        private Brush _foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private Brush _background = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        public ICommand DecreaseValue { get; private set; }
        public ICommand IncreaseValue { get; private set; }

        private TextBox InputTextBox;

        public NumericUpDownViewModel(TextBox inputBox)
        {
            InputTextBox = inputBox;

            InitCommands();

            Text = Value.ToString();
        }

        private void InitCommands()
        {
            this.DecreaseValue = new RelayCommand((_) =>
            {
                this.Value = EnsureValueConstraints(Value - StepSize);
            });
            this.IncreaseValue = new RelayCommand((_) =>
            {
                this.Value = EnsureValueConstraints(Value + StepSize);
            });
        }
        private float EnsureValueConstraints(float newValue)
        {
            float result = newValue;

            if (newValue > MaxValue)
                result = MaxValue;
            else if (newValue < MinValue)
                result = MinValue;

            return result;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
