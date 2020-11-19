using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GCodeViewer.Library.PrinterSettings
{
    public class Settings
    {
        public PrinterDimensions PrinterDimensions { get; set; } = new PrinterDimensions();
        public AAxisParserInfo AAxisParserInfo { get; set; } = new AAxisParserInfo();
        public CAxisParserInfo CAxisParserInfo { get; set; } = new CAxisParserInfo();

        public async Task Save(FileStream stream)
        {
            var writer = new StreamWriter(stream);

            var text = JsonConvert.SerializeObject(this, _settings);

            await writer.WriteAsync(text);
            writer.Close();
        }

        public static Settings Load(FileStream stream)
        {
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd().Trim();
            reader.Close();

            return JsonConvert.DeserializeObject<Settings>(text, _settings);
        }

        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };
    }
}
