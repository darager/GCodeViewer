using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GCodeViewer.Library
{
    public class Configuration
    {
        public PrintArea PrintArea { get; set; } = new PrintArea();

        private static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public async Task Save(FileStream stream, CancellationToken token)
        {
            var writer = new StreamWriter(stream);


            var text = JsonConvert.SerializeObject(this, _settings);

            await writer.WriteAsync(text);
            writer.Close();
        }
        public static Configuration Load(FileStream stream)
        {
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd().Trim();
            reader.Close();

            return JsonConvert.DeserializeObject<Configuration>(text, _settings);
        }
    }
}
