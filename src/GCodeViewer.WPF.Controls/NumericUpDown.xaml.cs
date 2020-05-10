using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GCodeViewer.WPF.MVVM.Helpers;
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
                typeof(float), typeof(NumericUpDown),
                new FrameworkPropertyMetadata(default(float),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public float MinValue
        {
            get => (float)this.GetValue(MinValueProperty);
            set => this.SetValue(MinValueProperty, value);
        }
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register(
                "MinValue",
                typeof(float), typeof(NumericUpDown),
                new PropertyMetadata(0.0f));

        public float MaxValue
        {
            get => (float)this.GetValue(MaxValueProperty);
            set => this.SetValue(MaxValueProperty, value);
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register(
                "MaxValue",
                typeof(float), typeof(NumericUpDown),
                new PropertyMetadata(100.0f));

        public float StepSize
        {
            get => (float)this.GetValue(StepSizeProperty);
            set => this.SetValue(StepSizeProperty, value);
        }
        public static readonly DependencyProperty StepSizeProperty =
            DependencyProperty.Register(
                "StepSize",
                typeof(float), typeof(NumericUpDown),
                new PropertyMetadata(10.0f));

        new public Brush Background
        {
            get => (Brush)this.GetValue(BackgroundProperty);
            set => this.SetValue(BackgroundProperty, value);
        }
        new public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush), typeof(NumericUpDown),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(239, 239, 245))));
        new public Brush Foreground
        {
            get => (Brush)this.GetValue(ForegroundProperty);
            set => this.SetValue(ForegroundProperty, value);
        }

        new public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                "Foreground",
                typeof(Brush), typeof(NumericUpDown),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(51, 51, 77))));
        #endregion

        public ICommand DecreaseValue { get; private set; }
        public ICommand IncreaseValue { get; private set; }

        public NumericUpDown()
        {
            InitializeComponent();
            InitCommands();

            rootElement.DataContext = this;
        }

        private void InitCommands()
        {
            this.DecreaseValue = new RelayCommand((_) =>
            {
                // Apparently Binding is Broken if you do not use SetCurrentValue
                SetCurrentValue(ValueProperty, EnsureValueConstraints(Value - StepSize));
            });
            this.IncreaseValue = new RelayCommand((_) =>
            {
                SetCurrentValue(ValueProperty, EnsureValueConstraints(Value + StepSize));
            });
        }
        private float EnsureValueConstraints(float newValue)
        {
            float result = newValue;

            if (newValue > MaxValue)
                result = MaxValue;
            else if (newValue < MinValue)
                result = MinValue;

            return result;
        }
    }
}
