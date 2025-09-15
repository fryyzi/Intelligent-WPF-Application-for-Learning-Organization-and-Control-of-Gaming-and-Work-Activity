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
            var numberText = NumberTexBox.Text;

            if (!double.TryParse(numberText, out double number))
            {
                MessageBox.Show("Введіть коректне число.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            var item = TimeComboBox.SelectedItem as string;
            {
                var index = Array.IndexOf(TimeComboBox.ItemsSource.Cast<string>().ToArray(), item);
                double seconds = 0;
                switch (index)
                {
                    case 0:
                        seconds = number;
                        break;
                    case 1:
                        seconds = number * 60;
                        break;
                    case 2:
                        seconds = number * 3600;
                        break;
                    case 3:
                        seconds = number * 86400;
                        break;
                    case 4:
                        seconds = number * 604800;
                        break;
                    case 5:
                        seconds = number * 2592000;
                        break;
                    case 6:
                        seconds = number * 31536000;
                        break;
                    default:
                        MessageBox.Show("Виберіть коректну одиницю виміру.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }
                SecundeTextBlock.Text = seconds != 0
                    ? $"Секунди: {seconds:F2}"
                    : " ";

                MinuteTextBlock.Text = (seconds / 60.0) != 0
                    ? $"Хвилини: {(seconds / 60.0):F2}"
                    : " ";

                HourTextBlock.Text = (seconds / 3600.0) != 0
                    ? $"Години: {(seconds / 3600.0):F2}"
                    : " ";

                DayTextBlock.Text = (seconds / 86400.0) != 0
                    ? $"Дні: {(seconds / 86400.0):F2}"
                    : " ";

                WeekTextBlock.Text = (seconds / 604800.0) != 0
                    ? $"Тижні: {(seconds / 604800.0):F2}"
                    : " ";

                MonthTextBlock.Text = (seconds / 2592000.0) != 0
                    ? $"Місяці: {(seconds / 2592000.0):F2}"
                    : " ";

                YearTextBlock.Text = (seconds / 31536000.0) != 0
                    ? $"Роки: {(seconds / 31536000.0):F2}"
                    : " ";
            }

        }
    }
}
