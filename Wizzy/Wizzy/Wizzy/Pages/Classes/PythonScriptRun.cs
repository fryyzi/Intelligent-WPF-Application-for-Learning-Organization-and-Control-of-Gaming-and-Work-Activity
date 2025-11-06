using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wizzy.Pages.Classes
{
    public class PythonScriptRun
    {
        public void StartVoiceServer()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = "Voices.py",
                    WorkingDirectory = @"F:\programing\Project\Wizzy\Wizzy\Scripts\Python",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                };
                var process = new Process();
                process.StartInfo = psi;
                process.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (s, e) => Console.WriteLine("Python error: " + e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                MessageBox.Show("🎤 Python voice server started!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка запуску Python: " + ex.Message);   
            }
        }
    }
}
