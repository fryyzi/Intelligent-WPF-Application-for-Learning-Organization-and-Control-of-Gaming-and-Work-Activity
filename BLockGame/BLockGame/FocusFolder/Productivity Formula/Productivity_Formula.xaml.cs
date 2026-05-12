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

namespace BLockGame.FocusFolder.Productivity_Formula
{
    public partial class Productivity_Formula : Window
    {

        private DispatcherTimer _timer;
        private DateTime _StartTimer;
        private TimeSpan _timeLeft;

        private bool _isBreakMode = true;


        public Productivity_Formula()
        {
            InitializeComponent();

            _timeLeft = new TimeSpan(0, 54, 0);
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > TimeSpan.Zero)
            {
                _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
            }
            else
            {
                if (!_isBreakMode)
                {
                    _timeLeft = new TimeSpan(0, 17, 0);
                    StatusText.Text = "ПЕРЕРВА";
                    _isBreakMode = true;
                }
                else
                {
                    _timeLeft = new TimeSpan(0, 54, 0);
                    _timeLeft = _timeLeft.Subtract(TimeSpan.FromSeconds(1));
                    _isBreakMode = false;
                    StatusText.Text = "WAVE FLOW";
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

            _isBreakMode = false;
            _StartTimer = DateTime.Now;

            _timer.Start();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            TimerText.Text = "00:54:00";
            _timer.Stop();

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
