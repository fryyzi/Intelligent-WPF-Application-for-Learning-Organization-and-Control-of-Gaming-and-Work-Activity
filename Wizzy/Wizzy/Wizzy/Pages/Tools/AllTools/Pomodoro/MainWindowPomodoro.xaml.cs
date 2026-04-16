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
            EditColors();
            Timer();
            UpdateTimerLabel();

            var settings = MongoClientSettings.FromConnectionString(
               "mongodb://localhost:27017/"
           );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _imageSavePomodoro = database.GetCollection<ImageDocument>("SaveImageSetings");

            string NameImage = "";
            var ContentImage = dataBase.ViewImagePomodoro();
            if (ContentImage != null)
            {
                foreach (var item in ContentImage)
                {
                    NameImage = item.GetType().Name;
                }
            }

            var result = _imageSavePomodoro.Find(Builders<ImageDocument>.Filter.Empty).ToList();
            string Name = "test";
            foreach (var item in result)
            {
                File.WriteAllBytes(Name, item.ImageData);

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();

                using (var stream = new FileStream(Name, FileMode.Open, FileAccess.Read))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }

                imageBrush.ImageSource = bitmap;
                this.Background = imageBrush;
            }
        }

        private void EditColors()
        {
            var content = dataBase.ViewColorPomodoro();

            if (content != null)
            {
                foreach (var item in content)
                {
                    var colorValue = item.GetValue("ColorData");
                    MessageBox.Show("Test");
                    if (colorValue == "White")
                    {
                        TimerLabel.Foreground = Brushes.White;
                    }
                    if (colorValue == "Black")
                    {
                        TimerLabel.Foreground = Brushes.Black;
                    }
                }
            }
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
                SessionCountLabel.Content = $"{CountSessions = +1}";
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
                    //item.Contains("WorkTime"
                    if (double.TryParse(item.LongBreakTime, out double result))
                    {
                        WorkTime = result;
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
            if (Pages.DataBase.DataBaseTime.CountSessions == 0)
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
            Pages.Tools.AllTools.Pomodoro.SetingsFolder.SettingsWindow settingsWindow = new Pages.Tools.AllTools.Pomodoro.SetingsFolder.SettingsWindow();
            settingsWindow.Show();
        }

        private void SettingControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
