using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
using Microsoft.Win32;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder
{
    public partial class AddImage : Window
    {

        private IMongoCollection<ImageDocument> _collectionImagePomodoro;
        private IMongoCollection<ImageDocument> _CollectionSaveImage;
        Pages.DataBase.DataBase dataBase = new DataBase.DataBase();

        public class ImageDocument
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string Name { get; set; }
            public byte[] ImageData { get; set; }
        }

        public AddImage()
        {
            InitializeComponent();

            var settings = MongoClientSettings.FromConnectionString(
               "mongodb://localhost:27017/"
            );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _collectionImagePomodoro = database.GetCollection<ImageDocument>("Image");
            _CollectionSaveImage = database.GetCollection<ImageDocument>("SaveSetings");
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (dialog.ShowDialog() == true)
            {
                UrlImage.Text = dialog.FileName;
            }

            string path = UrlImage.Text;

            if (File.Exists(path))
            {
                byte[] imageBytes = File.ReadAllBytes(path);
                var img = new ImageDocument
                {
                    Name = System.IO.Path.GetFileName(path),
                    ImageData = imageBytes
                };
                _collectionImagePomodoro.InsertOne(img);
                MessageBox.Show("Зображення додано!");
            }
            else
            {
                MessageBox.Show("Файл не знайдено!");
            }
        }
    }
}
