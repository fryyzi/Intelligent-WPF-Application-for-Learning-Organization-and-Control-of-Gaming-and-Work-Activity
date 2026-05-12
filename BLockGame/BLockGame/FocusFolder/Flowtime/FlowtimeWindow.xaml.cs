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
namespace BLockGame.FocusFolder.Flowtime
{
    /// <summary>
    /// Interaction logic for FlowtimeWindow.xaml
    /// </summary>
    public partial class FlowtimeWindow : Window
    {
        private DispatcherTimer _timer;
        private DateTime _StartTimer;
        private TimeSpan _timeLeft;
        private bool _isBreakMode = false;


        public FlowtimeWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_isBreakMode)
            {
                TimeSpan elapsed = DateTime.Now - _StartTimer;
                TimerText.Text = elapsed.ToString(@"hh\:mm\:ss");
            }
            else
            {
                if (_timeLeft > TimeSpan.Zero)
                {
                    _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                    TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    _timer.Stop();
                    _isBreakMode = false;
                    TextTime.Text = "WAVE FLOW";
                }
            }
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _isBreakMode = false;
            _StartTimer = DateTime.Now;

            _timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _StartTimer;
            _isBreakMode = true;
            TextTime.Text = "Перерва";

            if (elapsed <= TimeSpan.FromSeconds(25))
                _timeLeft = TimeSpan.FromSeconds(5);
            else if (elapsed <= TimeSpan.FromMinutes(50))
                _timeLeft = TimeSpan.FromMinutes(9);
            else if (elapsed <= TimeSpan.FromMinutes(90))
                _timeLeft = TimeSpan.FromMinutes(10);
            else
                _timeLeft = TimeSpan.FromMinutes(20);
        }

        /*private void TimerPause()
        {
            TimeSpan elapsed = DateTime.Now - _StartTimer;
            TextTime.Text = "Перерва";

            if (elapsed <= TimeSpan.FromSeconds(5))
                _timeLeft = TimeSpan.FromMinutes(5);
            else if (elapsed <= TimeSpan.FromMinutes(50))
                _timeLeft = TimeSpan.FromMinutes(9);
            else if (elapsed <= TimeSpan.FromMinutes(90))
                _timeLeft = TimeSpan.FromMinutes(10);
            else
                _timeLeft = TimeSpan.FromMinutes(20);
        }*/


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
