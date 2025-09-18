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
        Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();

        public AddToDo()
        {
            InitializeComponent();
            dataBase.Connect();
        }

        private void AddButtonNewToDoListClick(object sender, RoutedEventArgs e)
        {
            newToDoText = NewToDoTextBox.Text.Trim();
            newMainToDoListName = NewMainToDoListTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newToDoText) || string.IsNullOrEmpty(newMainToDoListName))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.");
                return;
            }
            DataBase.DataBase.Text = newToDoText;
            DataBase.DataBase.ToDoListName = newMainToDoListName;

            dataBase.AddToDoList(newToDoText, newMainToDoListName);
        }
        
    }
}
