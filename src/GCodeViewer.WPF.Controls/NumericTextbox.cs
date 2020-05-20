using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GCodeViewer.Helpers;

namespace GCodeViewer.WPF.Controls
{
    public class NumericTextbox : TextBox
    {
        public float MinValue
        {
            get => (float)this.GetValue(MinValueProperty);
            set => this.SetValue(MinValueProperty, value);
        }
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(
                "MinValue",
                typeof(float), typeof(NumericTextbox),
                new PropertyMetadata(float.NaN));

        public float MaxValue
        {
            get => (float)this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(
                "MaxValue",
                typeof(float), typeof(NumericTextbox),
                new PropertyMetadata(float.NaN));

        public NumericTextbox() : base()
        {
            this.KeyDown += UnFocusIfEnter;
            this.GotFocus += SetCursorToEnd;
            this.TextChanged += EnsureValidNumber;
            this.PreviewTextInput += OnlyAllowValidCharacters;

            _previousText = this.Text;
        }

        private string _previousText;
        private void EnsureValidNumber(object sender, TextChangedEventArgs e)
        {
            ResetIfInvalidInput(_previousText);
            AdjustCursorPosition();

            _previousText = this.Text;
        }
        private void ResetIfInvalidInput(string previousText)
        {
            if (NotValidNumber(this.Text))
            {
                int cursorPos = this.SelectionStart;

                this.Text = previousText;

                SetCursorPosition(cursorPos);
            }
        }
        private void AdjustCursorPosition()
        {
            if (IsNotEmptyOrMinus(this.Text))
            {
                int cursorPos = this.SelectionStart;

                this.Text = EnsureValueConstraints(this.Text);

                SetCursorPosition(cursorPos);
            }
        }
        private string EnsureValueConstraints(string currentText)
        {
            float currentValue = float.Parse(this.Text);
            float constrainedValue = currentValue.Constrain(MinValue, MaxValue);

            return constrainedValue.ToString();
        }
        private bool IsNotEmptyOrMinus(string text)
        {
            return !string.IsNullOrEmpty(text)
                && !(this.Text == "-");
        }

        private Regex _inputPattern = new Regex("[-\\d\\.]");
        private void OnlyAllowValidCharacters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_inputPattern.IsMatch(e.Text);
        }

        private Regex _numberPattern = new Regex("^-?\\d*(\\.\\d*)?$");
        private bool NotValidNumber(string text)
        {
            return !_numberPattern.IsMatch(text);
        }
        private void SetCursorPosition(int position)
        {
            this.SelectionStart = position;
            this.SelectionLength = 0;
        }

        private void SetCursorToEnd(object sender, RoutedEventArgs e)
        {
            int lastPos = this.Text.Length;
            SetCursorPosition(lastPos);
        }

        private void UnFocusIfEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Keyboard.ClearFocus();
        }
    }
}
