using BLockGame.Class;
using System;
using System.Windows;
using System.Windows.Threading;

namespace BLockGame.FocusFolder.Flowtime
{
    public partial class FlowtimeWindow : Window
    {
        private readonly DispatcherTimer _timer;
        private DateTime _startTimer;
        private TimeSpan _timeLeft;
        private bool _isBreakMode = false;

        public FlowtimeWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += Timer_Tick;

            TimerText.Text = "00:00:00";
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (!_isBreakMode)
            {
                TimeSpan elapsed = DateTime.Now - _startTimer;
                TimerText.Text = elapsed.ToString(@"hh\:mm\:ss");
            }
            else
            {
                if (_timeLeft > TimeSpan.Zero)
                {
                    _timeLeft -= TimeSpan.FromSeconds(1);
                    TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    _timer.Stop();

                    _isBreakMode = false;
                    TextTime.Text = "WAVE FLOW";

                    TimerText.Text = "00:00:00";
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _isBreakMode = false;
            TextTime.Text = "Фокус";

            _startTimer = DateTime.Now;

            if (!_timer.IsEnabled)
                _timer.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _startTimer;

            AddNumberFocus.UpdateFlowtimeWindow();

            _isBreakMode = true;
            TextTime.Text = "Перерва";

            if (elapsed <= TimeSpan.FromMinutes(25))
            {
                _timeLeft = TimeSpan.FromMinutes(5);
            }
            else if (elapsed <= TimeSpan.FromMinutes(50))
            {
                _timeLeft = TimeSpan.FromMinutes(8);
            }
            else if (elapsed <= TimeSpan.FromMinutes(90))
            {
                _timeLeft = TimeSpan.FromMinutes(10);
            }
            else
            {
                _timeLeft = TimeSpan.FromMinutes(20);
            }

            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _timer?.Stop();
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _timer?.Stop();
        }
    }
}