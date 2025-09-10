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

namespace Wizzy.Pages.Tools.AllTools
{
    /// <summary>
    /// Логика взаимодействия для CalculatorTool.xaml
    /// </summary>
    public partial class CalculatorTool : Window
    {

        
        public CalculatorTool()
        {
            InitializeComponent();
        }

        private void Result_Click(object sender, RoutedEventArgs e)
        {
            int Number_One = int.Parse(FirstNumberTextBox.Text);
            int Number_Two = int.Parse(SecondNumberTextBox.Text);
            int Result = 0;
            string select = (OperationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (select == "+")
            {
                Result = Number_One + Number_Two;
            }
            if (select == "-")
            {
                Result = Number_One - Number_Two;
            }
            if (select == "*")
            {
                Result = Number_One * Number_Two;
            }
            if (select == "/")
            {
                Result = Number_One / Number_Two;
            }
            if(Result >= 0)
            {
                ResultLabel.Content = Result;
            }
        }

    }
}
