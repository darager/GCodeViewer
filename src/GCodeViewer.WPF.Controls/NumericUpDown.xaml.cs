using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GCodeViewer.WPF.Controls
{
    public partial class NumericUpDown : UserControl
    {
        #region DependencyProperties
        public float Value
        {
            get => (float)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(float),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateValue));
        private static void UpdateValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.Value = (float)e.NewValue;
        }

        public float MinValue
        {
            get => (float)this.GetValue(MinValueProperty);
            set => this.SetValue(MinValueProperty, value);
        }
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(
                "MinValue",
                typeof(float),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateMinValue));
        private static void UpdateMinValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.MinValue = (float)e.NewValue;
        }

        public float MaxValue
        {
            get => (float)this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(
                "MaxValue",
                typeof(float),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateMaxValue));
        private static void UpdateMaxValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.MaxValue = (float)e.NewValue;
        }

        public float StepSize
        {
            get => (float)this.GetValue(StepSizeProperty);
            set => this.SetValue(StepSizeProperty, value);
        }
        public static readonly DependencyProperty StepSizeProperty =
            DependencyProperty.Register(
                "StepSize",
                typeof(float),
                typeof(NumericUpDown), new FrameworkPropertyMetadata(UpdateStepSize));
        private static void UpdateStepSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sender = d as NumericUpDown;
            sender.ViewModel.StepSize = (float)e.NewValue;
        }

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
