﻿using System.Windows;

namespace OpenTkTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new PointCloudViewModel();
        }
    }
}