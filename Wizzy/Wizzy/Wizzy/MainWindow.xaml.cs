using NAudio.Wave;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Diagnostics;

using Wizzy.Pages;
using Wizzy.Pages.AIChat;
using Wizzy.Pages.Classes;
using Wizzy.Pages.HomeWork;
using Wizzy.Pages.tools;
using Wizzy.Pages.Tools.AllTools.Converns;

namespace Wizzy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            //Pages.Classes.Voices voice = new Pages.Classes.Voices();
            //voice.StartListeningAsync();


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var audioFile = new AudioFileReader(@"F:\programing\C# Programs\Wizzy\Wizzy\Sound\Open.wav");
            audioFile.Volume = 0.1f;

            var outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();



        }
        private void ToDoListButton(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ToDoList();
        }
        private void AddToDoList(object sender, RoutedEventArgs e)
        {
            Pages.ToDo.AddToDo viewContentTools = new Pages.ToDo.AddToDo();
            viewContentTools.Show();

        }
        private void ToolsButton(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ToolsContent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddHomeWork(object sender, RoutedEventArgs e)
        {
            Pages.HomeWork.AddWork addWork = new Pages.HomeWork.AddWork();
            addWork.Show();
        }

        private void ViewHomeWork(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeWork();
        }

        private void AiChatButton_Click(object sender, RoutedEventArgs e)
        {
            Pages.AIChat.AiChat aIChatPage = new Pages.AIChat.AiChat();
            aIChatPage.Show();
        }

        private void BlockApp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = @"F:\programing\Project\Block_Game\BLockGame\BLockGame\bin\Debug\net8.0-windows\BLockGame.exe",
                UseShellExecute = false
            });
        }
    }
}