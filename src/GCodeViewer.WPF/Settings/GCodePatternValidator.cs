using System;
using System.Globalization;
using System.Windows.Controls;

namespace GCodeViewer.WPF.Settings
{
    public class GCodePatternValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                // TODO: make sure validation works correctly
                string text = value as string;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Non valid GCodePattern!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
