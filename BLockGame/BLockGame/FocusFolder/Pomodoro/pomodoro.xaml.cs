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

namespace BLockGame.FocusFolder.Pomodoro
{
    /// <summary>
    /// Interaction logic for pomodoro.xaml
    /// </summary>
    public partial class pomodoro : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _timeRemaining;
        private bool _isRunning = false;
        private bool _isWorkingSession = true;
        private int _completedCycles = 0;

        private const int WorkTimeMin = 25;
        private const int BreakTimeMin = 5;
        public pomodoro()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            ResetTimer();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeRemaining.TotalSeconds > 0)
            {
                _timeRemaining = _timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimerDisplay();
            }
            else
            {
                _timer.Stop();
                _isRunning = false;
                StartPauseButton.Content = "Старт";

                StartPauseButton.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CAF50");

                if (_isWorkingSession)
                {
                    _completedCycles++;
                    CycleText.Text = $"Завершено циклів: {_completedCycles}";
                    //MessageBox.Show("Час для відпочинку! Відкладіть роботу.", "Помодоро", MessageBoxButton.OK, MessageBoxImage.Information);

                    _isWorkingSession = false;
                    StatusText.Text = "Час відпочити";
                    StatusText.Foreground = System.Windows.Media.Brushes.LightGreen;
                    _timeRemaining = TimeSpan.FromMinutes(BreakTimeMin);
                }
                else
                {
                    //MessageBox.Show("Перерва завершилася! Повертаємось до роботи.", "Помодоро", MessageBoxButton.OK, MessageBoxImage.Information);

                    _isWorkingSession = true;
                    StatusText.Text = "Час фокусуватися!";
                    StatusText.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6B6B");
                    _timeRemaining = TimeSpan.FromMinutes(WorkTimeMin);
                }

                UpdateTimerDisplay();
            }
        }

        private void StartPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isRunning)
            {
                _timer.Stop();
                StartPauseButton.Content = "Старт";
                StartPauseButton.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CAF50");
            }
            else
            {
                _timer.Start();
                StartPauseButton.Content = "Пауза";
                StartPauseButton.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF9800");
            }

            _isRunning = !_isRunning;
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _isRunning = false;
            _isWorkingSession = true;
            StartPauseButton.Content = "Старт";
            StartPauseButton.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CAF50");

            ResetTimer();
        }
        private void ResetTimer()
        {
            _timeRemaining = TimeSpan.FromMinutes(WorkTimeMin);
            StatusText.Text = "Час фокусуватися!";
            StatusText.Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6B6B");
            UpdateTimerDisplay();
        }
        private void UpdateTimerDisplay()
        {
            TimerText.Text = _timeRemaining.ToString(@"mm\:ss");
        }
    }
}
