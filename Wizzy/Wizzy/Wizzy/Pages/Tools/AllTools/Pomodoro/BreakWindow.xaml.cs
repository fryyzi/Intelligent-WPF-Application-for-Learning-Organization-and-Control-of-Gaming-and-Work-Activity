using System;
using System.Collections.Generic;
using System.Linq;
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
using Pomodoro = Wizzy.Pages.DataBase.DataBaseTime;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro
{
    public partial class BreakWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _BreakTime;

        Pages.DataBase.DataBase DataBase = new Pages.DataBase.DataBase();

        private double ShortBreakTime = 0.1;
        public BreakWindow()
        {
            InitializeComponent();
            Timer();
        }
        private void Timer()
        {
            var Content = DataBase.ViewTimePomodoro();

            if (Content != null)
            {
                foreach (var item in Content)
                {
                    //item.Contains("ShortBreakTime")
                    if (double.TryParse(item.ShortBreakTime, out double result))
                    {
                        ShortBreakTime = result;
                    }
                }

            }
            if (ShortBreakTime > 0)
            {
                _BreakTime = TimeSpan.FromMinutes(ShortBreakTime);
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += Timer_Tick;
                _timer.Start();
            }
            else
            {
                MessageBox.Show("Час для короткої перерви не знайдено в базі");
            }


        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_BreakTime > TimeSpan.Zero)
            {
                _BreakTime = _BreakTime.Add(TimeSpan.FromSeconds(-1));
                UpdateTimerLabel();
            }
            else
            {
                Pages.DataBase.DataBaseTime.CountSessions = 1;
                MainWindowPomodoro mainWindowPomodoro = new MainWindowPomodoro();
                mainWindowPomodoro.Show();
                _timer.Stop();
                this.Close();
            }
        }
        private void UpdateTimerLabel()
        {
            TimeLable.Content = _BreakTime.ToString(@"mm\:ss");
        }
    }
}
