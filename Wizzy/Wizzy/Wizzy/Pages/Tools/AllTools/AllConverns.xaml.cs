using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wizzy.Pages.Tools.AllTools.Converns;  

namespace Wizzy.Pages.Tools.AllTools
{
    /// <summary>
    /// Логика взаимодействия для AllConverns.xaml
    /// </summary>
    public partial class AllConverns : Window
    {
        public AllConverns()
        {
            InitializeComponent();
        }

        private void LeghtButton_Click(object sender, RoutedEventArgs e)
        {
            ConvernsContent.Content = new Legth();
        }

        private void MassButton_Click(object sender, RoutedEventArgs e)
        {
            ConvernsContent.Content = new Mass(); 
        }
    }
}
