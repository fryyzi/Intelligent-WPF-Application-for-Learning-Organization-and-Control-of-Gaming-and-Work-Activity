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
    /// Interaction logic for ViewWork.xaml
    /// </summary>
    public partial class ViewWork : Window
    {

        Wizzy.Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();
        string TitleCode = DataBase.DataBase.HomeWorkTitle;


        public ViewWork()
        {
            InitializeComponent();
            
            dataBase.Connect();

            var Content = dataBase.ViewHomeWork();

            foreach (var item in Content)
            {
                var Title = item.GetValue("Назва").AsString;
                var DescriptionText = item.GetValue("Опис").AsString;
                if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(DescriptionText))
                {
                    this.Close();
                }

                if (TitleCode == Title)
                {
                    TitleHomeWorkTextBlock.Text = Title;
                    DescriptionHomeWorkTextBox.Text = DescriptionText;
                }
            }

            

        }
    }
}
