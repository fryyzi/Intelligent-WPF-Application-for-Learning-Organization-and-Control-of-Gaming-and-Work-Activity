using BLockGame.Class;
using System;
using System.Windows;
using System.Windows.Threading;
using WpfMessageBox = System.Windows.MessageBox;

namespace BLockGame.FocusFolder.UltraFocus
{
    public partial class UltraFocus : Window
    {
        private readonly DispatcherTimer timer;
        private TimeSpan _timeLeft;

        private bool _isFocusMode;

        private readonly BlockApp _blockApp;

        public UltraFocus()
        {
            InitializeComponent();

            _blockApp = new BlockApp();
            _blockApp.StartMonitoring(Base_User.User, 3, "NoBD");

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += Timer_Tick;

            _timeLeft = TimeSpan.FromMinutes(30);
            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");

            _isFocusMode = true;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_timeLeft > TimeSpan.Zero)
            {
                _timeLeft -= TimeSpan.FromSeconds(1);
                TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
                return;
            }

            if (_isFocusMode)
            {
                AddNumberFocus.UpdateUltraFocus();
                _timeLeft = TimeSpan.FromMinutes(2);
                _isFocusMode = false;

                WpfMessageBox.Show("Час фокусування завершено!", "Ultra Focus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _timeLeft = TimeSpan.FromMinutes(30);
                _isFocusMode = true;

                WpfMessageBox.Show("Час фокусування завершено!", "Ultra Focus", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            _timeLeft = TimeSpan.FromMinutes(30);
            _isFocusMode = true;

            TimerText.Text = _timeLeft.ToString(@"hh\:mm\:ss");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            timer?.Stop();
        }
    }
}