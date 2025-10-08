using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder
{
    /// <summary>
    /// Interaction logic for BackGroundSetting.xaml
    /// </summary>
    public partial class BackGroundSetting : Window
    {

        private IMongoCollection<ImageDocument> _collectionImagePomodoro;
        private IMongoCollection<ImageDocument> _imageSavePomodoro;
        Pages.DataBase.DataBase dataBase = new DataBase.DataBase();

        public BackGroundSetting()
        {
            InitializeComponent();

            var settings = MongoClientSettings.FromConnectionString(
               "mongodb://localhost:27017/"
           );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _collectionImagePomodoro = database.GetCollection<ImageDocument>("Image");
            _imageSavePomodoro = database.GetCollection<ImageDocument>("SaveSetings");

            var result = _collectionImagePomodoro.Find(Builders<ImageDocument>.Filter.Empty).ToList();

            int i = 1;
            foreach (var item in result)
            {
                string fileName = $"output_{i}.png";
                File.WriteAllBytes(fileName, item.ImageData);

                Image image = new Image
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(5),
                    Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(fileName), UriKind.Absolute))
                };

                image.MouseLeftButtonDown += (s, e) =>
                {
                    ImageBrush imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath(fileName), UriKind.Absolute)),
                        Stretch = Stretch.UniformToFill
                    };

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window is MainWindowPomodoro mainWindowPomodoro)
                        {
                            mainWindowPomodoro.Background = imageBrush;
                            break;
                        }
                    }
                    string path = fileName;
                    var contentImage = dataBase.ViewImagePomodoro();
                    string NameSave = "BackGround";
                    string ViewName = "";
                    if (contentImage != null)
                    {
                        foreach (var item in contentImage)
                        {
                            ViewName = item.Name;
                        }
                        if(NameSave == ViewName)
                        {
                            byte[] imageBytes = File.ReadAllBytes(path);
                            var filters = Builders<ImageDocument>.Filter.Eq("Name", "BackGround");
                            var update = Builders<ImageDocument>.Update
                                .Set("ImageData", imageBytes);
                            _imageSavePomodoro.UpdateOne(filters, update);
                        }
                        else
                        {
                            if (File.Exists(path))
                            {
                                byte[] imageBytes = File.ReadAllBytes(path);
                                var img = new ImageDocument
                                {
                                    Name = System.IO.Path.GetFileName(NameSave.ToString()),
                                    ImageData = imageBytes
                                };
                                _imageSavePomodoro.InsertOne(img);
                            }
                            else
                            {
                                MessageBox.Show("Файл не знайдено!");
                            }
                        }
                    }
                };

                MainGrid.Children.Add(image);
                i++;
            }

        }

        private void TestBackGround_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            AddImage image = new AddImage();
            image.Show();
            this.Close();
        }
    }
}
