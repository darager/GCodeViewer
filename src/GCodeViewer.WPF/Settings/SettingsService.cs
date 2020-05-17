﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GCodeViewer.Library;

namespace GCodeViewer.WPF.Settings
{
    public class SettingsService
    {
        private string _filePath;

        private string _fileName = "gcodeviewer-config.json";
        private string _diretoryName = "Data";

        public SettingsService()
        {
            _filePath = Path.Combine(_diretoryName, _fileName);

            CreateConfigurationFileIfNoneExists().Wait();
        }

        private async Task CreateConfigurationFileIfNoneExists()
        {
            if (File.Exists(_filePath))
                return;

            if (!Directory.Exists(_diretoryName))
                Directory.CreateDirectory(_diretoryName);

            var config = new Configuration();
            await StoreSettings(config);
        }

        public Configuration LoadSettings()
        {
            using var stream = File.OpenRead(_filePath);
            var config = Configuration.Load(stream);
            stream.Close();

            return config;
        }

        public async Task StoreSettings(Configuration settings)
        {
            using var stream = File.OpenWrite(_filePath);

            await settings.Save(stream, CancellationToken.None);
            stream.Close();
        }
    }
}