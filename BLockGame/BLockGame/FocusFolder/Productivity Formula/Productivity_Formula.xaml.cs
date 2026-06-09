using BLockGame.Class;
using System;
using System.Windows;
using System.Windows.Threading;

namespace BLockGame.FocusFolder.Productivity_Formula
{
    public partial class Productivity_Formula : Window
    {
        private readonly DispatcherTimer _timer;
        private TimeSpan _timeLeft;

        private bool _isFocusMode = true;

        private const int FocusMinutes = 5;
        private const int BreakMinutes = 17;

        public Productivity_Formula()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += Timer_Tick;

            StartFocus();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > TimeSpan.Zero)
            {
                _timeLeft -= TimeSpan.FromSeconds(1);
                TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
                return;
            }

            if (_isFocusMode)
            {
                AddNumberFocus.UpdateProductivity_Formula();

                _isFocusMode = false;

                _timeLeft = TimeSpan.FromMinutes(BreakMinutes);
                StatusText.Text = "ПЕРЕРВА";
            }
            else
            {
                _isFocusMode = true;

                _timeLeft = TimeSpan.FromMinutes(FocusMinutes);
                StatusText.Text = "WAVE FLOW";
            }

            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            StartFocus();
        }

        private void StartFocus()
        {
            _isFocusMode = true;
            _timeLeft = TimeSpan.FromMinutes(FocusMinutes);

            StatusText.Text = "WAVE FLOW";
            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Close();
        }
    }
}