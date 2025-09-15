using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Wizzy.Pages.DataBase;
using static System.Net.Mime.MediaTypeNames;

namespace Wizzy.Pages.ToDo
{
    public partial class ViewContentToDoList : Window
    {
        Wizzy.Pages.DataBase.DataBase database = new Wizzy.Pages.DataBase.DataBase();
        string Title = String.Empty;
        string TextDoTo = String.Empty;
        string Maintext = DataBase.DataBase.TitleMAainText;

        public ViewContentToDoList()
        {
            InitializeComponent();
            database.Connect();

            var Content = database.ViewContentToDoList();

            foreach (var item in Content) 
            {
                Title = item.GetValue("ToDoListName").AsString;
                TextDoTo = item.GetValue("Text").AsString;

                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(TextDoTo))
                {
                    this.Close();
                }

                if(Maintext == TextDoTo)
                {
                    TitleToDoListTextBlock.Text = Title;
                    ContentToDoListTextBox.Text = TextDoTo;
                }
            }
        }
    }
}
