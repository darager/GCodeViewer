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
            _vm.NumValue = 50;

            this.DataContext = _vm;
        }
    }
}
