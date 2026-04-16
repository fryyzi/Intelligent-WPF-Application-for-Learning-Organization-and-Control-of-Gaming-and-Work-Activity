using ICSharpCode.AvalonEdit.Highlighting;
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
        string Code = String.Empty;

        string Maintext = DataBase.DataBase.TitleMainText;

        public ViewContentToDoList()
        {
            InitializeComponent();
            database.Connect();

            var Content = database.ViewContentToDoList();

            foreach (var item in Content)
            {
                Title = item.ToDoListName;
                TextDoTo = item.Text;
                Code = item.IsCode;

                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(TextDoTo))
                {
                    this.Close();
                }

                if (Maintext == Title)
                {
                    TitleToDoListTextBlock.Text = Title;
                    if (Code != "None")
                    {
                        CodeSynTextBox.Text = TextDoTo;
                        CodeSynTextBox.Visibility = Visibility.Visible;
                        ContentToDoListTextBox.Visibility = Visibility.Hidden;
                        if (Code == "C#")
                        {
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("C#");
                        }
                        if (Code == "HTML")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("HTML");
                        if (Code == "XML")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XML");
                        if (Code == "JavaScript")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("JavaScript");
                        if (Code == "SQL")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("SQL");
                        if (Code == "PHP")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("PHP");
                        if (Code == "VB")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("VB");
                        if (Code == "XAML")
                            CodeSynTextBox.SyntaxHighlighting = HighlightingManager.Instance.GetDefinition("XAML");
                    }
                    else
                    {
                        CodeSynTextBox.Visibility = Visibility.Hidden;
                        ContentToDoListTextBox.Visibility = Visibility.Visible;
                        ContentToDoListTextBox.Text = TextDoTo;
                    }

                }
            }
        }

        private void EditToDoListButton_Click(object sender, RoutedEventArgs e)
        {
            ToDo.EditToDoList editToDoList = new ToDo.EditToDoList();
            editToDoList.Show();
        }
    }
}
