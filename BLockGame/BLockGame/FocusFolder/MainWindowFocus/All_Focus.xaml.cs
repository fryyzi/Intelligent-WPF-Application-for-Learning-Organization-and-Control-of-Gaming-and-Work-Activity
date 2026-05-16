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

namespace BLockGame.FocusFolder.MainWindowFocus
{
    public partial class All_Focus : Window
    {

        private BlockApp _blockApp;

        public All_Focus()
        {
            InitializeComponent();

            _blockApp = new BlockApp();

        }

        private void StartPomodoro_Click(object sender, RoutedEventArgs e)
        {
            FocusFolder.Pomodoro.pomodoro pomodoroWindow = new FocusFolder.Pomodoro.pomodoro();
            pomodoroWindow.Show();
        }

        private void StartFlowtime_Click(object sender, RoutedEventArgs e)
        {
            FocusFolder.Flowtime.FlowtimeWindow flowtimeWindow = new FocusFolder.Flowtime.FlowtimeWindow();
            flowtimeWindow.Show();
        }

        private void Start5217_Click(object sender, RoutedEventArgs e)
        {
            FocusFolder.Productivity_Formula.Productivity_Formula productivity_Formula = new FocusFolder.Productivity_Formula.Productivity_Formula();
            productivity_Formula.Show();
        }

        private void StartDeepWork_Click(object sender, RoutedEventArgs e)
        {
            FocusFolder.UltraFocus.UltraFocus ultrafocus = new FocusFolder.UltraFocus.UltraFocus();
            ultrafocus.Show();
        }

        private void StartGamingMode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CustomMode_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
