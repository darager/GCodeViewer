using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GCodeViewer.Library;

namespace UserControlTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new NumUpDownViewModel();

            TestOutConfigFunctionality().Wait();
        }

        private Configuration _config;

        private async Task TestOutConfigFunctionality()
        {
            string fileName = "gcodeviewer-config.json";

            string directoryPath = "Configuration";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fileName);

            bool fileExists = File.Exists(filePath);

            if (!fileExists)
            {
                using var stream = File.Create(filePath);

                var config = new Configuration();
                await config.Save(stream, CancellationToken.None);
                stream.Close();
            }
            else
            {
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                _config = Configuration.Load(stream);
                stream.Close();
            }
        }
    }
}
