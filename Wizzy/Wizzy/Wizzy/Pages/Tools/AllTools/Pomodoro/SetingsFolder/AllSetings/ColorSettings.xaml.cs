using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
using Wizzy.Pages.DataBase;
using static Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder.AddImage;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder.AllSetings
{
    /// <summary>
    /// Interaction logic for ColorSettings.xaml
    /// </summary>
    public partial class ColorSettings : UserControl
    {
        Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();
        private IMongoCollection<BsonDocument> _ColorSavePomodoro;


        public ColorSettings()
        {
            InitializeComponent();
            var settings = MongoClientSettings.FromConnectionString(
               "mongodb://localhost:27017/"
           );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _ColorSavePomodoro = database.GetCollection<BsonDocument>("SaveColorsSetings");

        }

        private void WhiteButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.AddColorPomodoro("White");
            MainWindowPomodoro mainWindow = Application.Current.Windows.OfType<MainWindowPomodoro>().FirstOrDefault();
            mainWindow.TimerLabel.Foreground = Brushes.White;
        }

        private void BlackButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.AddColorPomodoro("Black");
            MainWindowPomodoro mainWindow = Application.Current.Windows.OfType<MainWindowPomodoro>().FirstOrDefault();
            mainWindow.TimerLabel.Foreground = Brushes.Black;
        }
    }
}
