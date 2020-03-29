using System.Windows;
using System.Collections.ObjectModel;
using GCodeViewer.OpenTK.Helpers.Renderables;
using System.Drawing;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // HACK
            this.pclViewer.Renderables = new ObservableCollection<Renderable>();
            float[] _coordinateSytemVertices =
            {
                0.0f, 0.0f, 0.0f,   0.1f, 0.0f, 0.0f, // X
                0.0f, 0.0f, 0.0f,   0.0f, 0.1f, 0.0f, // Y
                0.0f, 0.0f, 0.0f,   0.0f, 0.0f, 0.1f  // Z
            };
            var coordSystem = new Renderable(Color.Red, _coordinateSytemVertices, RenderableType.Lines);
            this.pclViewer.Renderables.Add(coordSystem);

            //this.pclViewer.Renderables = new ObservableCollection<Renderable>();
            //this.DataContext = new PointCloudViewModel();
        }
    }
}
