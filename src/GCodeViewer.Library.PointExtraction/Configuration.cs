using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GCodeViewer.Library
{
    public class Configuration
    {
        public PrintArea PrintArea { get; set; } = new PrintArea();

        public async Task Save(FileStream stream, CancellationToken token)
        {
            var writer = new StreamWriter(stream);

            var formatSettings = new JsonSerializerSettings();
            formatSettings.Formatting = Formatting.Indented;

            var text = JsonConvert.SerializeObject(this, formatSettings);

            await writer.WriteAsync(text);
            writer.Close();
        }
        public static Configuration Load(FileStream stream)
        {
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();
            reader.Close();

            var formatSettings = new JsonSerializerSettings();
            formatSettings.Formatting = Formatting.Indented;

            return JsonConvert.DeserializeObject<Configuration>(text, formatSettings);
        }
    }

    public class PrintArea
    {
        public float Diameter { get; set; } = 100;
        public float Height { get; set; } = 100;
    }
}
