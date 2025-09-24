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

namespace Wizzy.Pages.HomeWork
{
    /// <summary>
    /// Interaction logic for EditHomeWork.xaml
    /// </summary>
    public partial class EditHomeWork : Window
    {
        DataBase.DataBase dataBase = new DataBase.DataBase();
        public EditHomeWork()
        {
            InitializeComponent();
            dataBase.Connect();

            var Content = dataBase.ViewHomeWork();
            foreach (var item in Content)
            {
                var MainName = item.GetValue("Назва").AsString;
                var DespenshionText = item.GetValue("Опис").AsString;
                if (string.IsNullOrEmpty(MainName) || string.IsNullOrEmpty(DespenshionText))
                {
                    this.Close();
                }
                if (DataBase.DataBase.HomeWorkTitle == MainName)
                {
                    EditTitleHomeWorkTextBlock.Text = MainName;
                    EditDespenshionTextBlock.Text = DespenshionText;
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Connect();
            string MyTitle = EditTitleHomeWorkTextBlock.Text;

            if (string.IsNullOrEmpty(MyTitle))
            {
                MessageBox.Show("Поле не може бути пустим!");
                return;
            }

            DataBase.DataBase.TitleMainHomeWork = MyTitle; 
            dataBase.DeleteDataBase(2);
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Connect();                 
            string MyTitle = EditTitleHomeWorkTextBlock.Text;
            string MyDespenshion = EditDespenshionTextBlock.Text;

            if(string.IsNullOrEmpty(MyTitle) || string.IsNullOrEmpty(MyDespenshion))
            {
                MessageBox.Show("Поля не можуть бути пустими!");
                return;
            }

            dataBase.UpdateDataBase(MyTitle, MyDespenshion, 2);
        }
    }
}
