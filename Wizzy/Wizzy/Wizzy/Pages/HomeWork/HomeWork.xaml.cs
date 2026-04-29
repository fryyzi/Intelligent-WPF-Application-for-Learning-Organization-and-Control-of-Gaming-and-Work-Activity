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
using Wizzy.Pages.DataBase.Model;

namespace Wizzy.Pages.HomeWork
{
    /// <summary>
    /// Interaction logic for HomeWork.xaml
    /// </summary>
    public partial class HomeWork : UserControl
    {
        private List<HomeWorkModel> _homeWorks = new List<HomeWorkModel>();
        public HomeWork()
        {
            Wizzy.Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();

            InitializeComponent();

            dataBase.Connect();

            var Content = dataBase.ViewHomeWork();

            foreach (var item in Content)
            {
                var TextMain = item.Name;
                var DescriptionText = item.Description;
                var DeadLineText = item.DeadLine;
                var Time = item.Time;

                _homeWorks.Add(new HomeWorkModel
                {
                    Name = TextMain,
                    Description = DescriptionText,
                    DeadLine = DeadLineText,
                    Time = Time
                });

                MyGrid.ItemsSource = null;
                MyGrid.ItemsSource = _homeWorks;
            }




        }
    }
}
