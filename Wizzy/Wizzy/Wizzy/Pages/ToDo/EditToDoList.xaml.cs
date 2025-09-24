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

namespace Wizzy.Pages.ToDo
{
    /// <summary>
    /// Логика взаимодействия для EditToDoList.xaml
    /// </summary>
    public partial class EditToDoList : Window
    {
        DataBase.DataBase database = new DataBase.DataBase();
        string Maintext = DataBase.DataBase.TitleMainText;
        public EditToDoList()
        {
            InitializeComponent();
            
            database.Connect();
            

            var Content = database.ViewContentToDoList();
            foreach(var item in Content)
            {
                string Title = item.GetValue("ToDoListName").AsString;
                string TextDoTo = item.GetValue("Text").AsString;
                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(TextDoTo))
                {
                    this.Close();
                }
                if (Maintext == Title)
                {
                    EditTitleToDoListTextBlock.Text = Title;
                    EditTextBlock.Text = TextDoTo;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            database.Connect();

            string NewTitle = EditTitleToDoListTextBlock.Text;
            string NewText = EditTextBlock.Text;

            if (string.IsNullOrEmpty(NewTitle) || string.IsNullOrEmpty(NewText))
            {
                MessageBox.Show("Поля не можуть бути пустими!");
                return;
            }
            database.UpdateDataBase(NewTitle, NewText, 1);
            MessageBox.Show("Зміни збережено!");
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DataBase database = new DataBase.DataBase();
            database.Connect();

            database.DeleteDataBase(1);
            this.Close();
        }
    }
}
