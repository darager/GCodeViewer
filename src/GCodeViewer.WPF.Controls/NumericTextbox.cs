using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            this.PreviewTextInput += OnlyAllowValidCharacters;
            this.TextChanged += EnsureValidNumber;

            _previousText = this.Text;
        }

        private Regex _inputPattern = new Regex("[-\\d\\.]");
        private void OnlyAllowValidCharacters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_inputPattern.IsMatch(e.Text);
        }

        private string _previousText;
        private void EnsureValidNumber(object sender, TextChangedEventArgs e)
        {
            if (NotValidNumber(this.Text))
            {
                int pos = this.SelectionStart;

                this.Text = _previousText;

                SetCursorPosition(pos);
            }

            if (IsNotEmptyOrMinus(this.Text))
            {
                int pos = this.SelectionStart;

                this.Text = EnsureValueConstraints(this.Text);

                SetCursorPosition(pos);
            }

            _previousText = this.Text;
        }

        private bool IsNotEmptyOrMinus(string text)
        {
            return !string.IsNullOrEmpty(text)
                && !(this.Text == "-");
        }
        private string EnsureValueConstraints(string currentText)
        {
            string result = currentText;

            float currentValue = float.Parse(this.Text);
            if (currentValue > MaxValue)
                result = MaxValue.ToString();
            else if (currentValue < MinValue)
                result = MinValue.ToString();

            return result;
        }

        private Regex _numberPattern = new Regex("^-?\\d*(\\.\\d*)?$");
        private bool NotValidNumber(string text)
        {
            return !_numberPattern.IsMatch(text);
        }
        private void SetCursorPosition(int position)
        {
            this.SelectionStart = position;
            this.SelectionLength = position;
        }
    }
}
