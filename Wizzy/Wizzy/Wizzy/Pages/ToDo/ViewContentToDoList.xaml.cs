using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;
using Wizzy.Pages.DataBase;

namespace Wizzy.Pages.ToDo
{
    /// <summary>
    /// Логика взаимодействия для ViewContentToDoList.xaml
    /// </summary>
    public partial class ViewContentToDoList : Window
    {
         Wizzy.Pages.DataBase.DataBase database = new Wizzy.Pages.DataBase.DataBase();
        public ViewContentToDoList()
        {
            InitializeComponent();

            //TitleToDoListTextBlock.Text = DataBase.DataBase.ToDoListName;
            //ContentToDoListTextBox.Text = DataBase.DataBase.Text;
            database.Connect();

            var Content = database.ViewContentToDoList();

            foreach(var item in Content) 
            {
                TitleToDoListTextBlock.Text = item.GetValue("ToDoListName").AsString;
                ContentToDoListTextBox.Text = item.GetValue("Text").AsString;

            }
        }  
    }
}
