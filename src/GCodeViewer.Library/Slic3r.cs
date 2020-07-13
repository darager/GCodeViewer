using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GCodeViewer.Library
{
    public class Slic3r
    {
        public string ExecutablePath { get; private set; }

        public Slic3r(string executablePath)
        {
            ExecutablePath = executablePath;
        }

        public async Task<string> Help()
        {
            return await RunCommand("--help").ReadToEndAsync();
        }

        private StreamReader RunCommand(string cmd, bool hidden = true)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();

            startInfo.WindowStyle = hidden ? ProcessWindowStyle.Hidden
                                           : ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = $"/C {ExecutablePath} {cmd}";
            process.StartInfo = startInfo;

            process.Start();

            return process.StandardOutput;
        }
    }
}
