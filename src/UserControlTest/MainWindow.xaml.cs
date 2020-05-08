using System.ComponentModel;
using System.Windows;

namespace UserControlTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new NumUpDownViewModel();
            this.DataContext = vm;
            vm.NumValue = 111;
        }
    }

    public class NumUpDownViewModel : INotifyPropertyChanged
    {
        public float NumValue
        {
            get => _value;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumValue"));
            }
        }
        private float _value;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
