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

namespace Wizzy.Pages
{
    /// <summary>
    /// Логика взаимодействия для ToDoList.xaml
    /// </summary>
    public partial class ToDoList : UserControl
    {
        public ToDoList()
        {
            InitializeComponent();
            Wizzy.Pages.DataBase.DataBase database = new Wizzy.Pages.DataBase.DataBase();
            database.Connect();



            var Content = database.ViewContentToDoList();

            foreach (var item in Content)
            {
                var TextMain = item.ToDoListName;

                TextBlock textBlock = new TextBlock
                {
                    Text = TextMain,
                    FontSize = 16,
                    Margin = new Thickness(10),
                    Cursor = Cursors.Hand
                };

                textBlock.MouseLeftButtonDown += (s, e) =>
                {
                    DataBase.DataBase.TitleMainText = TextMain;
                    var addToDoWindow = new Pages.ToDo.ViewContentToDoList();
                    addToDoWindow.Show();
                };

                Text.Children.Add(new Border
                {
                    Background = Brushes.LightGray,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5),
                    Margin = new Thickness(10),
                    VerticalAlignment = VerticalAlignment.Top,
                    Child = textBlock
                });

            }
        }
    }
}
