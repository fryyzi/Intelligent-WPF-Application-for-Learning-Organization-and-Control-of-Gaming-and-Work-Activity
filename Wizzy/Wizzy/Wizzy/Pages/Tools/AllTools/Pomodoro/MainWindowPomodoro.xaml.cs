using MongoDB.Driver;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wizzy.Pages.DataBase;
using Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder.AddImage;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindowPomodoro.xaml
    /// </summary>
    public partial class MainWindowPomodoro : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _timeLeft;

        Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();
        private IMongoCollection<ImageDocument> _imageSavePomodoro;




        int CountSessions = 0;
        public MainWindowPomodoro()
        {
            InitializeComponent();
            Timer();
            UpdateTimerLabel();

            var settings = MongoClientSettings.FromConnectionString(
               "mongodb://localhost:27017/"
           );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _imageSavePomodoro = database.GetCollection<ImageDocument>("SaveImage");

            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > TimeSpan.Zero)
            {
                _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
                UpdateTimerLabel();
            }
            else
            {
                SessionCountLabel.Content = $"{CountSessions =+ 1}";
                BreakWindow breakWindow = new BreakWindow();
                breakWindow.Show();
                _timer.Stop();
                this.Close();
            }
        }
        private void SettingsTimer()
        {
            double WorkTime = 0;
            var content = dataBase.ViewTimePomodoro();

            if (content != null)
            {
                foreach (var item in content)
                {
                    if (item.Contains("WorkTime"))
                    {
                        WorkTime = item.GetValue("WorkTime").ToDouble();
                    }
                }
            }

            if (WorkTime > 0)
            {

                _timeLeft = TimeSpan.FromMinutes(WorkTime);
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
            }
            else
            {
                _timeLeft = TimeSpan.FromMinutes(25);
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
            }

        }

        private void Timer()
        {
            if(Pages.DataBase.DataBaseTime.CountSessions == 0)
            {
                SettingsTimer();
            }
            else
            {
                SettingsTimer();
                Task.Delay(1000).ContinueWith(_ =>
                {
                    _timer.Start();
                });
            }
            
        }

        private void UpdateTimerLabel()
        {
            TimerLabel.Content = _timeLeft.ToString(@"mm\:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_timer.IsEnabled)
                _timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
                _timer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timeLeft = TimeSpan.FromMinutes(25);
            UpdateTimerLabel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            



        }

        private void SettingControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}
