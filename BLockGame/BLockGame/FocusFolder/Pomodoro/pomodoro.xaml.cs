using BLockGame.Class;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace BLockGame.FocusFolder.Pomodoro
{
    public partial class pomodoro : Window
    {
        private readonly DispatcherTimer _timer;

        private TimeSpan _timeRemaining;
        private bool _isRunning;
        private bool _isWorkingSession = true;
        private int _completedCycles;

        private const int WorkTimeMin = 25;
        private const int BreakTimeMin = 5;


        public pomodoro()
        {
            InitializeComponent();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += Timer_Tick;

            ResetTimer();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_timeRemaining > TimeSpan.Zero)
            {
                _timeRemaining -= TimeSpan.FromSeconds(1);
                UpdateTimerDisplay();
                return;
            }

            _timer.Stop();
            _isRunning = false;

            StartPauseButton.Content = "Старт";

            if (_isWorkingSession)
            {
                _completedCycles++;

                AddNumberFocus.UpdatePomodoro();

                CycleText.Text = $"Завершено циклів: {_completedCycles}";

                _isWorkingSession = false;

                StatusText.Text = "Час відпочити";

                _timeRemaining = TimeSpan.FromMinutes(BreakTimeMin);
            }
            else
            {
                _isWorkingSession = true;

                StatusText.Text = "Час фокусуватися!";

                _timeRemaining = TimeSpan.FromMinutes(WorkTimeMin);
            }

            UpdateTimerDisplay();

            System.Windows.MessageBox.Show(
                _isWorkingSession
                    ? "Перерва завершена. Починаємо працювати!"
                    : "Робочий цикл завершено. Час відпочити!",
                "Pomodoro",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void StartPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isRunning)
            {
                _timer.Stop();

                StartPauseButton.Content = "Старт";
            }
            else
            {
                _timer.Start();

                StartPauseButton.Content = "Пауза";
            }

            _isRunning = !_isRunning;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();

            _isRunning = false;
            _isWorkingSession = true;

            StartPauseButton.Content = "Старт";

            ResetTimer();
        }

        private void ResetTimer()
        {
            _timeRemaining = TimeSpan.FromMinutes(WorkTimeMin);

            StatusText.Text = "Час фокусуватися!";

            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            TimerText.Text = _timeRemaining.ToString(@"mm\:ss");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _timer.Stop();
        }
    }
}