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
    /// <summary>
    /// Interaction logic for All_Focus.xaml
    /// </summary>
    public partial class All_Focus : Window
    {
        public All_Focus()
        {
            InitializeComponent();
        }

        private void StartPomodoro_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartFlowtime_Click(object sender, RoutedEventArgs e)
        {
            FocusFolder.Flowtime.FlowtimeWindow flowtimeWindow = new FocusFolder.Flowtime.FlowtimeWindow();
            flowtimeWindow.Show();
        }

        private void Start5217_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartDeepWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartGamingMode_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CustomMode_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
