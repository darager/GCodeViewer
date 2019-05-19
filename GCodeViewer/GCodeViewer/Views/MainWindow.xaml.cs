using GCodeViewer.RenderWindow.Utils;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace GCodeViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".gcode";
            openFileDialog.Filter = "gcode files (*.gcode)|*.gcode|txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string[] text = File.ReadAllLines(filePath);

                for(int i = 0; i < 10; i++)
                    textblock.AppendText(text[i] + "\n");

                await Task.Factory.StartNew(() =>
                {
                    //textblock.Text = File.ReadAllText(openFileDialog.FileName);
                });
            }
        }

        private void Textblock_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(e.Changes.ToString());
        }
    }
}
