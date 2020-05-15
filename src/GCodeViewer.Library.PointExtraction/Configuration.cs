using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GCodeViewer.Library
{
    public class Configuration
    {
        public PrintArea PrintArea { get; set; } = new PrintArea();

        public async Task SaveConfiguration(FileStream stream, CancellationToken token)
        {
            var writer = new StreamWriter(stream);
            var text = JsonConvert.SerializeObject(this);

            await writer.WriteAsync(text);
            writer.Close();
        }
        public async static Task<Configuration> LoadConfiguration(FileStream stream)
        {
            var reader = new StreamReader(stream);
            var text = await reader.ReadToEndAsync();
            reader.Close();

            return JsonConvert.DeserializeObject<Configuration>(text);
        }
    }

    public class PrintArea
    {
        public float Diameter { get; set; } = 100;
        public float Height { get; set; } = 100;
    }
}
