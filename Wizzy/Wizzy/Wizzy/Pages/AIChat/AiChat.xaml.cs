using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wizzy.Pages.AIChat
{
    /// <summary>
    /// Interaction logic for AiChat.xaml
    /// </summary>
    public partial class AiChat : Window
    {
        private Process pythonProcess;
        string SendMesage = "Send start message";
        public AiChat()
        {
            InitializeComponent();
            //StartPythonBot();
            AddMessage("Бот працює стабільно!", false);
            /*SendToPython(SendMesage);*/
        }
        private void AddMessage(string text, bool isUser)
        {
            var msg = new TextBlock
            {
                Text = text,
                Margin = new Thickness(10),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                Background = isUser ? Brushes.LightBlue : Brushes.LightGray,
                Padding = new Thickness(8),
                MaxWidth = 300
            };

            ChatPanel.Children.Add(msg);

            ChatScroll.ScrollToEnd();
        }

        /*public void StartPythonBot()
        {
            pythonProcess = new Process();
            pythonProcess.StartInfo = new ProcessStartInfo()
            {
                FileName = "python",
                Arguments = "F:\\programing\\Project\\Wizzy\\Wizzy\\Scripts\\Python\\AIMessage.py",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardInputEncoding = Encoding.UTF8
            };

            pythonProcess.OutputDataReceived += Python_OutputDataReceived;

            pythonProcess.Start();
            pythonProcess.BeginOutputReadLine();
        }*/
        /* private void Python_OutputDataReceived(object sender, DataReceivedEventArgs e)
         {
             if (string.IsNullOrWhiteSpace(e.Data))
                 return;

             if (e.Data.Trim() == "READY")
                 return;


             try
             {
                 var json = System.Text.Json.JsonDocument.Parse(e.Data);
                 var response = json.RootElement.GetProperty("response").GetString();
                 Dispatcher.Invoke(() =>
                 {
                     AddMessage("Bot: " + response, false);
                 });
             }
             catch
             {
                 // ignore parse errors
             }
         }*/
        public string SendMessage(string msg)
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 5050))
            using (NetworkStream stream = client.GetStream())
            {
                var json = JsonSerializer.Serialize(new { message = msg });
                byte[] data = Encoding.UTF8.GetBytes(json);

                // Відправляємо
                stream.Write(data, 0, data.Length);

                // Читаємо відповідь
                byte[] buffer = new byte[4096];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                string responseJson = Encoding.UTF8.GetString(buffer, 0, bytes);

                var response = JsonSerializer.Deserialize<JsonElement>(responseJson);

                return response.GetProperty("response").GetString();
            }
        }

        private void AiSend_Click(object sender, RoutedEventArgs e)
        {
            string message = AiInput.Text;

            AddMessage("You: " + message, true);
            SendMessage(message);

            AiInput.Clear();
        }
    }
}
