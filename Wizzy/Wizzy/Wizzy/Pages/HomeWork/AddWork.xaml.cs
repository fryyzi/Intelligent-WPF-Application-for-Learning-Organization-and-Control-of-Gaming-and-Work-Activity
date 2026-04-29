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
using Wizzy.Pages.DataBase;
using static System.Net.Mime.MediaTypeNames;

namespace Wizzy.Pages.HomeWork
{
    /// <summary>
    /// Interaction logic for AddWork.xaml
    /// </summary>
    /// 

    public partial class AddWork : Window
    {
        Wizzy.Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();

        public AddWork()
        {
            InitializeComponent();
            dataBase.Connect();
        }

        public void CheckText()
        {
            var TitleText = TitleHomeWorkTextBox.Text.Trim();
            var DescriptionText = DescriptionHomeWorkTextBox.Text.Trim();
            DateTime? dateTime = DeadlineDatePicker.SelectedDate;
            if (dateTime == null)
            {
                MessageBox.Show("Оберіть дату");
            }
            TimeSpan timeSpan;
            if (!TimeSpan.TryParse(DeadlineTimeTextBox.Text.Trim(), out timeSpan))
            {
                MessageBox.Show("Невірний формат часу. Введіть час у формате ЧЧ:ММ");
                return;
            }


            if (string.IsNullOrEmpty(TitleText))
            {
                MessageBox.Show("Ведіть назву");
                return;
            }
            if (string.IsNullOrEmpty(DescriptionText))
            {
                MessageBox.Show("Ведіть опис");
                return;
            }
            dataBase.AddHomeWork(TitleText, DescriptionText, DeadlineDatePicker.Text, DeadlineTimeTextBox.Text);
        }

        private void AddHomeWorkButton_Click(object sender, RoutedEventArgs e)
        {
            CheckText();
            this.Close();

            MessageBox.Show(DeadlineDatePicker.Text);
        }
    }
}
