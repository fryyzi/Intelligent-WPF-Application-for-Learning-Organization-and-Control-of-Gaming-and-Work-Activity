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
    }
}
