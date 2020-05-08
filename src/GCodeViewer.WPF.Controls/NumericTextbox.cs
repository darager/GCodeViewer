using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace GCodeViewer.WPF.Controls
{
    public class NumericTextbox : TextBox
    {
        // TODO: Handle constraints in this class

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

            _previousText = this.Text;
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
