using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace GCodeViewer.WPF.Controls
{
    public class NumericTextbox : TextBox
    {
        public NumericTextbox() : base()
        {
            this.PreviewTextInput += EnsureValidInput;
            this.TextChanged += NumericTextbox_TextChanged;
        }

        private Regex _inputPattern = new Regex("[-\\d\\.]");
        private void EnsureValidInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_inputPattern.IsMatch(e.Text);
        }

        private void NumericTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
