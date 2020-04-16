using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private PointCloudViewModel _vm = new PointCloudViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;

            TextEditor.TextChanged += (_, text) => _vm.Update3DModel(text);
            _vm.Update3DModel(TextEditor.Text);
        }
    }
}
