using System.Windows;

namespace UserControlTest
{
    public partial class MainWindow : Window
    {
        NumUpDownViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new NumUpDownViewModel();
            // TODO: when this is used the controls looks fine
            //       but when DataContext of the control itself is set nothing works anymore
            //       I am completely and utterly lost for words!  :'(
            this.DataContext = _vm;
            _vm.NumValue = 111;

            // getting and setting the value by hand works but the binding does not!!!
            //numUpDown.Value = 123;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float currentValue = numUpDown.Value;
        }
    }
}
