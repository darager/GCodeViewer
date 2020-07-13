using System.Diagnostics;
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

        public async Task<string> Help() => await RunSlicerCommand("--help");

        private async Task<string> RunSlicerCommand(string cmd, bool hidden = true)
        {
            return await RunCommand($"{ExecutablePath} {cmd}", hidden);
        }

        private async Task<string> RunCommand(string cmd, bool hidden = true)
        {
            var process = new Process();
            var startInfo = new ProcessStartInfo();

            startInfo.WindowStyle = hidden ? ProcessWindowStyle.Hidden
                                           : ProcessWindowStyle.Normal;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = $"/C {cmd}";
            process.StartInfo = startInfo;

            process.Start();

            return await process.StandardOutput.ReadToEndAsync();
        }
    }
}
