using BLockGame.Class;
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


namespace BLockGame.FocusFolder.UltraFocus
{
    public partial class UltraFocus : Window
    {
        private DispatcherTimer timer;
        private DateTime _StartTimer;
        private TimeSpan _timeLeft;

        private bool _isRunning = true;

        private BlockApp _blockApp;

        public UltraFocus()
        {
            InitializeComponent();

            _blockApp = new BlockApp();



            //BLockApp
            _blockApp.StartMonitoring(Base_User.User, 3, "NoBD");

            _timeLeft = new TimeSpan(0, 0, 1);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
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
                if (!_isRunning)
                {
                    //plus number focus
                    AddNumberFocus.UpdateUltraFocus();
                    _timeLeft = new TimeSpan(0, 30, 0);
                    _isRunning = true;
                }
                else
                {
                    _timeLeft = new TimeSpan(0, 0, 2);
                    _isRunning = false;
                }
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _StartTimer = DateTime.Now;
            timer.Start();
            _isRunning = false;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
