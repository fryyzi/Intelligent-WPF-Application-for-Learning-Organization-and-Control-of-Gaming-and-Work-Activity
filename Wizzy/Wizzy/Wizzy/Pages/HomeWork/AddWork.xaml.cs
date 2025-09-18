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
    public partial class AddWork : Window
    {
        Wizzy.Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();
        public AddWork()
        {
            InitializeComponent();
            dataBase.Connect();
        }

        private void AddHomeWorkButton_Click(object sender, RoutedEventArgs e)
        {
            var TitleText = TitleHomeWorkTextBox.Text.Trim();
            var DescriptionText = DescriptionHomeWorkTextBox.Text.Trim();

            if (string.IsNullOrEmpty(TitleText) || string.IsNullOrEmpty(DescriptionText))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.");
                return;
            }
            dataBase.AddHomeWork(TitleText, DescriptionText);

            this.Close();
        }
    }
}
