using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeViewer.WPF.Controls
{
    public class NumericUpDownViewModel : INotifyPropertyChanged
    {
        float Value;
        float MinValue;
        float MaxValue;
        float StepSize;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
