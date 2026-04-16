using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wizzy.Pages.DataBase;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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

            var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017/");
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _collectionImagePomodoro = database.GetCollection<ImageDocument>("Image");
            _imageSavePomodoro = database.GetCollection<ImageDocument>("SaveImageSetings");

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
                    Source = LoadImageFromFile(fileName)
                };

                image.MouseLeftButtonDown += (s, e) =>
                {
                    ImageBrush imageBrush = new ImageBrush
                    {
                        ImageSource = LoadImageFromFile(fileName),
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
                        foreach (var content in contentImage)
                        {
                            ViewName = content.Name;
                        }

                        byte[] imageBytes = File.ReadAllBytes(path);

                        if (NameSave == ViewName)
                        {
                            var filters = Builders<ImageDocument>.Filter.Eq("Name", "BackGround");
                            var update = Builders<ImageDocument>.Update
                                .Set("ImageData", imageBytes);
                            _imageSavePomodoro.UpdateOne(filters, update);
                        }
                        else
                        {
                            var img = new ImageDocument
                            {
                                Name = NameSave,
                                ImageData = imageBytes
                            };
                            _imageSavePomodoro.InsertOne(img);
                        }
                    }
                };

                MainGrid.Children.Add(image);
                i++;
            }
        }
        private BitmapImage LoadImageFromFile(string filePath)
        {
            var bitmap = new BitmapImage();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }
            bitmap.Freeze();
            return bitmap;
        }

        private void TestBackGround_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Connect();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (openFileDialog.ShowDialog() != true)
            {
                return;
            }

            string path = openFileDialog.FileName;

            if (!File.Exists(path))
            {
                MessageBox.Show("Файл не знайдено!");
                return;
            }

            byte[] ImageData = File.ReadAllBytes(path);
            string Name = System.IO.Path.GetFileName(path);

            dataBase.addImage(ImageData, Name);
        }
    }
}
