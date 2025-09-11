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

namespace Wizzy.Pages.Tools.AllTools.Converns.Time
{
    /// <summary>
    /// Логика взаимодействия для TimeWindow.xaml
    /// </summary>
    public partial class TimeWindow : UserControl
    {
        public TimeWindow()
        {
            InitializeComponent();
            TimeComboBox.ItemsSource = new List<string>
            {
                "Секунди",
                "Хвиилини",
                "Години",
                "Дні",
                "Тижні",
                "Місяці",
                "Роки",
            };
            TimeComboBox.SelectedIndex = 0;
        }

        private void ConvertTimeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
