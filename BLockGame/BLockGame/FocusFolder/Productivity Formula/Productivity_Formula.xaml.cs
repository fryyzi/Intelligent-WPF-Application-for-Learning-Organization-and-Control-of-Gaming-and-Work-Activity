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
    /// <summary>
    /// Interaction logic for Productivity_Formula.xaml
    /// </summary>
    public partial class Productivity_Formula : Window
    {

        private DispatcherTimer _timer;
        private DateTime _StartTimer;
        private TimeSpan _timeLeft;

        public Productivity_Formula()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            //_timer.Tick += Timer_Tick;
        }



    }
}
