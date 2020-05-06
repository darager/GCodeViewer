using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GCodeViewer.WPF.Controls
{
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
            var viewmodel = new NumericUpDownViewModel();

            // HACK: only for design time
            viewmodel.Foreground = new SolidColorBrush(Colors.White);
            viewmodel.Background = new SolidColorBrush(Colors.DarkGray);


            this.DataContext = viewmodel;
        }
    }
}
