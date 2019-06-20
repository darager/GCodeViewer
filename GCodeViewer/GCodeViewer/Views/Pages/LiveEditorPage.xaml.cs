using GCodeViewer.Abstractions.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace GCodeViewer.Views
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
            //ScreenSpaceLine3D line = new
            //liveRenderWindow.Children.Add();
        }
    }
}
