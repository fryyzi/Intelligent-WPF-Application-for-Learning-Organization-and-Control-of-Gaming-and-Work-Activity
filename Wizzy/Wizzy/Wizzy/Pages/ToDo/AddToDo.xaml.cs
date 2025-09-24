using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wizzy.Pages.DataBase;
using MongoDB.Driver;


namespace Wizzy.Pages.ToDo
{
    public partial class AddToDo : Window
    {
        string newToDoText = String.Empty;
        string newMainToDoListName = String.Empty;
        string IsCodeAddToDo = String.Empty;

        int IsNotCode = 0;

        Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();

        public AddToDo()
        {
            InitializeComponent();
            LanguageComboBox.ItemsSource = new List<string>
            {
                "C#",
                "HTML",
                "XML",
                "JavaScript",
                "SQL",
                "PHP",
                "VB",
                "XAML"
            };
            LanguageComboBox.SelectedIndex = 0;
        }

        private void AddButtonNewToDoListClick(object sender, RoutedEventArgs e)
        {
            dataBase.Connect();
            newToDoText = NewToDoTextBox.Text.Trim();
            newMainToDoListName = NewMainToDoListTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newToDoText) || string.IsNullOrEmpty(newMainToDoListName))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.");
                return;
            }
            DataBase.DataBase.Text = newToDoText;
            DataBase.DataBase.ToDoListName = newMainToDoListName;
            if (LanguageComboBox.SelectedItem != null && IsCodeCheckBox.IsChecked == true)
            {

                var item = LanguageComboBox.SelectedItem as string;
                {
                    IsCodeAddToDo = LanguageComboBox.SelectedItem.ToString();
                }
            }
            else
            {
                IsCodeAddToDo = "None";
            }
            dataBase.AddToDoList(newToDoText, newMainToDoListName, IsCodeAddToDo);

        }

        private void IsCodeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LanguageComboBox.Visibility = Visibility.Visible;
        }

        private void IsCodeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            LanguageComboBox.Visibility = Visibility.Hidden;
        }
    }
}
