using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;


namespace Wizzy.Pages.Classes
{
    public class Voices
    {
        public async Task StartListeningAsync()
        {

            Process.Start(new ProcessStartInfo
            {
                FileName = "python",
                Arguments = "Voices.py",
                WorkingDirectory = @"F:\programing\Project\Wizzy\Wizzy\Scripts\Python",
                UseShellExecute = false,
            });

            using (TcpClient client = new TcpClient("127.0.0.1", 5000))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string command = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    command = command.ToLower();
                    //MessageBox.Show("Команда з голосу: " + command);

                    if (command.Contains("відкрий меню"))
                    {
                        MessageBox.Show("➡ Відкриваю меню");
                    }
                    if (command.Contains("відкрий браузер") || command.Contains("відкрий brow") || command.Contains("відкрий бра") || command.Contains("відкрий брау"))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://google.com",
                            UseShellExecute = true
                        });
                    }
                    if (command.Contains("закрий браузер") || command.Contains("закрий brow") || command.Contains("закрий бра") || command.Contains("закрий брау"))
                    {
                        foreach (var process in Process.GetProcessesByName("chrome"))
                        {
                            process.Kill();
                        }
                        foreach (var process in Process.GetProcessesByName("firefox"))
                        {
                            process.Kill();
                        }
                    }
                    if (command.Contains("відкрий youtube") || command.Contains("відкрий ютуб"))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://youtube.com",
                            UseShellExecute = true
                        });
                    }
                    if (command.Contains("відкрий chat") || command.Contains("відкрий чат"))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://chatgpt.com",
                            UseShellExecute = true
                        });
                    }
                }
            }
        }
    }
}
