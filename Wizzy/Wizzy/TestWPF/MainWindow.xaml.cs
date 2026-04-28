using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var people = new List<Person>
            {
               new Person {Name = "Viktor", Age = 12 }
            };

            MyGrid.ItemsSource = people;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}