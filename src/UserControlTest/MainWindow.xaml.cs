using System.ComponentModel;
using System.Windows;

namespace UserControlTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new NumUpDownViewModel();
        }
    }

    public class NumUpDownViewModel : INotifyPropertyChanged
    {
        private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
