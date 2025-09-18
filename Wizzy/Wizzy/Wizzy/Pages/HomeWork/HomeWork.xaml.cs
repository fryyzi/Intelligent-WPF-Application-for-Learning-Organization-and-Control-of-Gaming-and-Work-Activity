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
    /// Interaction logic for HomeWork.xaml
    /// </summary>
    public partial class HomeWork : UserControl
    {
        public HomeWork()
        {
            Wizzy.Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();
            InitializeComponent();

            dataBase.Connect();

            var Content = dataBase.ViewHomeWork();

            foreach( var item in Content)
            {
                var TextMain = item.GetValue("Назва").AsString;


                TextBlock textBlock = new TextBlock
                {
                    Text = TextMain,
                    FontSize = 16,
                    Margin = new Thickness(10),
                    Cursor = Cursors.Hand
                };

                textBlock.MouseLeftButtonDown += (s, e) =>
                {
                    DataBase.DataBase.HomeWorkTitle = TextMain;
                    var addToDoWindow = new Pages.HomeWork.ViewWork();
                    addToDoWindow.Show();
                };

                TextHomeWork.Children.Add(new Border
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
