using System.Globalization;
using System.Windows.Controls;

namespace GCodeViewer.WPF.Settings
{
    public class GCodePatternValidator : ValidationRule
    {
        private string _regexPlaceholder = "{{value}}";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (value as string)
                                .Trim()
                                .Replace(" ", "");

            if (pattern.Contains(_regexPlaceholder) && pattern.Length > _regexPlaceholder.Length)
                return ValidationResult.ValidResult;

            return new ValidationResult(false, $"Non valid GCodePattern!");
        }
    }
}
