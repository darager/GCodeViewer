using GCodeViewer.WPF.Abstractions.ViewModels;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GCodeViewer.WPF.Views.Pages
{
    /// <summary>
    /// Interaction logic for LiveEditorPage.xaml
    /// </summary>
    public partial class LiveEditorPage : Page
    {
        public LiveEditorPage(ITextViewModel textViewModel)
        {
            InitializeComponent();
            this.DataContext = textViewModel;

            //TODO: remove this
            PopulateRenderWindow();
        }

        private void PopulateRenderWindow()
        {
            var pointExtractor = new PointExtractor(@"C:\Users\florager\source\repos\darager\GCodeViewer\GCodeViewer\Examples\SinkingBenchy.gcode");
            Point3DCollection points = pointExtractor.ExtractPoints();

            for (int i = 0; i < 10000; i++)
            {
                if (i % 10 == 0)
                    port.AddPoint(points[i].X, points[i].Y, points[i].Z, Colors.DarkRed, 1);
            }
        }
    }
}
