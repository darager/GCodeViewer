using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        private PointCloudViewModel vm = new PointCloudViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = vm;

            TextEditor.TextChanged += (s, text) => vm.Update3DModel(text);
        }
    }
}
