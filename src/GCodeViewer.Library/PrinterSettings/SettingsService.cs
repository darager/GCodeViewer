﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GCodeViewer.Library.PrinterSettings
{
    public class SettingsService
    {
        private string _filePath;

        private string _fileName = "config.json";
        private string _diretoryName = "Data";

        public Settings Settings { get; private set; }

        public SettingsService()
        {
            _filePath = Path.Combine(_diretoryName, _fileName);

            CreateConfigurationFileIfNoneExists().Wait();

            Settings = LoadSettings();
        }

        private async Task CreateConfigurationFileIfNoneExists()
        {
            if (File.Exists(_filePath))
                return;

            if (!Directory.Exists(_diretoryName))
                Directory.CreateDirectory(_diretoryName);

            var config = new Settings();
            await StoreSettings(config);
        }

        private Settings LoadSettings()
        {
            using var stream = File.OpenRead(_filePath);
            var config = Settings.Load(stream);
            stream.Close();

            return config;
        }

        public async Task StoreSettings(Settings settings)
        {
            File.Delete(_filePath);
            using var stream = File.Open(_filePath, FileMode.OpenOrCreate);

            await settings.Save(stream, CancellationToken.None);
            stream.Close();
        }
    }
}