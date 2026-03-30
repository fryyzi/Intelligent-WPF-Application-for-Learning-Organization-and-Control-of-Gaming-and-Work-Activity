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
    /// Interaction logic for AwarageScore.xaml
    /// </summary>
    public partial class AwarageScore : Window
    {
        public AwarageScore()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SumTextBlock.Text, out int sum))
            {
                MessageBox.Show("Введіть коректну суму оцінок!");
                return;
            }

            if (!int.TryParse(NumberOfRaiting.Text, out int count))
            {
                MessageBox.Show("Введіть коректну кількість оцінок!");
                return;
            }

            if (count <= 0)
            {
                MessageBox.Show("Кількість оцінок повинна бути більше 0!");
                return;
            }

            double result = (double)sum / count;

            MessageBox.Show($"Середній бал: {result:F2}");

        }
    }
}
