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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wizzy.Pages.tools
{
    /// <summary>
    /// Логика взаимодействия для ToolsContent.xaml
    /// </summary>
    public partial class ToolsContent : UserControl
    {
        public ToolsContent()
        {
            InitializeComponent();
        }

        private void TimerButton_Click(object sender, RoutedEventArgs e)
        {
            var timerWindow = new Pages.Tools.AllTools.TImer();
            timerWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calculator_Click(object sender, RoutedEventArgs e)
        {
            var calculatorWindow = new Tools.AllTools.CalculatorTool();
            calculatorWindow.Show();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            var ConvertWindow = new Tools.AllTools.AllConverns();
            ConvertWindow.Show();
        }

        private void PomodoroButton_Click(object sender, RoutedEventArgs e)
        {
            var PomodoroWindow = new Tools.AllTools.Pomodoro.MainWindowPomodoro();
            PomodoroWindow.ShowDialog();
        }

        private void PasswordGenerator_Click(object sender, RoutedEventArgs e)
        {
            string path = "F:\\programing\\Project\\GenerationPassword\\Generation password\\bin\\Debug\\net8.0-windows\\Generation password.exe";
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //var AwardWindow = new Tools.AllTools.AwarageScore();
            //AwardWindow.ShowDialog();
        }
    }
}
