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
        #region DependencyProperties
        new public Brush Background
        {
            get => (Brush)this.GetValue(BackgroundProperty);
            set => this.SetValue(BackgroundProperty, value);
        }
        new public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateBackground));
        private static void UpdateBackground(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.Background = (Brush)e.NewValue;
        }

        new public Brush Foreground
        {
            get => (Brush)this.GetValue(ForegroundProperty);
            set => this.SetValue(ForegroundProperty, value);
        }
        new public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                "Foreground",
                typeof(Brush),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateForeground));
        private static void UpdateForeground(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.Foreground = (Brush)e.NewValue;
        }
        #endregion

        internal NumericUpDownViewModel ViewModel = new NumericUpDownViewModel();

        public NumericUpDown()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
