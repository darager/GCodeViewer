using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace GCodeViewer.Library.Slicing
{
    public class Slic3r
    {
        public string ExecutablePath { get; private set; }

        public Slic3r(string executablePath)
        {
            ExecutablePath = executablePath;
        }

        public async Task<string> Help() => await RunSlicerCommand("--help");

        public async Task<string> ToObj(string stlFilePath, string resultingPath)
        {
            return await RunSlicerCommand($"--export-obj {stlFilePath} -o {resultingPath}");
        }

        public async Task Slice(string stlFilePath, string gcodeFilePath)
        {
            await RunSlicerCommand($"{stlFilePath} --layer-height 0.1 --output {gcodeFilePath}");
        }

        public async Task<string> RunSlicerCommand(string cmd)
        {
            return await RunCommand(cmd).ReadToEndAsync();
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
