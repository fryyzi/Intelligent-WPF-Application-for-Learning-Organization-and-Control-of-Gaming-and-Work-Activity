using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wizzy.Pages.tools;


namespace Wizzy.Pages.Classes
{
    public class Voices
    {
        public async Task StartListeningAsync()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
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

                    switch (command)
                    {
                        case string s when s.Contains("відкрий браузер") || s.Contains("відкрий brow") || s.Contains("відкрий бра") || s.Contains("відкрий брау"):
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "https://google.com",
                                UseShellExecute = true
                            });
                            break;
                        case string s when s.Contains("закрий браузер") || s.Contains("закрий brow") || s.Contains("закрий бра") || s.Contains("закрий брау"):
                            foreach (var process in Process.GetProcessesByName("chrome"))
                            {
                                process.Kill();
                            }
                            foreach (var process in Process.GetProcessesByName("firefox"))
                            {
                                process.Kill();
                            }
                            break;
                        case string s when s.Contains("відкрий youtube") || s.Contains("відкрий ютуб"):
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "https://youtube.com",
                                UseShellExecute = true
                            });
                            break;
                        case string s when s.Contains("відкрий chat") || s.Contains("відкрий чат"):
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "https://chatgpt.com",
                                UseShellExecute = true
                            });
                            break;


                        case string s when s.Contains("відкрий нотатки"):
                            
                            mainWindow.MainContent.Content = new ToDoList();
                            break;

                        case string s when s.Contains("відкрити інструменти"):
                            mainWindow.MainContent.Content = new ToolsContent();
                            break;

                        case string s when s.Contains("добавити нотатку"):
                            Pages.ToDo.AddToDo viewContentTools = new Pages.ToDo.AddToDo();
                            viewContentTools.Show();
                            break;


                        case string s when s.Contains("відкрити калькулятор"):
                            var calculatorWindow = new Tools.AllTools.CalculatorTool();
                            calculatorWindow.Show();
                            break;

                        case string s when s.Contains("відкрити таймер"):
                            var timerWindow = new Pages.Tools.AllTools.TImer();
                            timerWindow.Show();
                            break;
                        case string s when s.Contains("відкрити помодоро"):
                            var PomodoroWindow = new Tools.AllTools.Pomodoro.MainWindowPomodoro();
                            PomodoroWindow.ShowDialog();
                            break;
                        case string s when s.Contains("відкрити генератор паролів"):
                            string path = "F:\\programing\\Project\\GenerationPassword\\Generation password\\bin\\Debug\\net8.0-windows\\Generation password.exe";
                            System.Diagnostics.Process.Start("explorer.exe", path);
                            break;
                        case string s when s.Contains("відкрити конвертер"):
                            var ConvertWindow = new Tools.AllTools.AllConverns();
                            ConvertWindow.Show();
                            break;


                        default:
                            break;  

                    }
                }
            }
        }
    }
}
