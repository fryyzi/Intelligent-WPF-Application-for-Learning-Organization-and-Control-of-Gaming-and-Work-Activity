using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using Wizzy.Pages.DataBase.Model;
using static Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder.AddImage;


namespace Wizzy.Pages.DataBase
{
    class DataBase
    {
        public static string Text = "Not Text";
        public static string ToDoListName = "No Name ToDoList";

        public static string HomeWorkTitle = "No Name HomeWork";
        public static string IsCode = string.Empty;

        public static string TitleMainText = " ";
        public static string TitleMainHomeWork = "";

        public static int IdToDo = 0;
        public static int IsCodeDataBase;

        public static string WorkTimeDataBase = "";

        public List<ToDoModel> ToDos { get; set; } = new List<ToDoModel>();
        public List<HomeWorkModel> HomeWorks { get; set; } = new List<HomeWorkModel>();
        public List<TimePomodoroModel> TimePomodoros { get; set; } = new List<TimePomodoroModel>();
        public List<AddImageModel> ImagePomodoros { get; set; } = new List<AddImageModel>();
        public List<BsonDocument> SaveColorsSetings { get; set; } = new List<BsonDocument>();

        private ToDoModel _toDoModel;
        private HomeWorkModel _homeWorkModel;
        private TimePomodoroModel _timePomodoroModel;
        private ImageDocument _imageDocument;
        private BsonDocument _bsonDocument;

        private IMongoCollection<ToDoModel> _collectionToDo;//complited
        private IMongoCollection<HomeWorkModel> _collectionHomeWork;//complited
        private IMongoCollection<TimePomodoroModel> _collectionTimePomodoro;
        private IMongoCollection<ImageDocument> _collectionImagePomodoro;
        private IMongoCollection<BsonDocument> _SaveColorsSetings;



        public void Connect()
        {
            var settings = MongoClientSettings.FromConnectionString(
                "mongodb://localhost:27017/"
            );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _collectionToDo = database.GetCollection<ToDoModel>("ToDoLists");
            _collectionHomeWork = database.GetCollection<HomeWorkModel>("HomeWork");
            _collectionTimePomodoro = database.GetCollection<TimePomodoroModel>("Time");
            _collectionImagePomodoro = database.GetCollection<ImageDocument>("Image");
            _SaveColorsSetings = database.GetCollection<BsonDocument>("SaveColorsSetings");
        }
        public void NoConnect()
        {
            if (_collectionToDo == null || _collectionHomeWork == null || _collectionTimePomodoro == null)
                Connect();
        }

        public void UpdateDataBase(string newTitle, string newText, int NumberFunction)
        {
            NoConnect();

            switch (NumberFunction)
            {
                case 1:
                    var filter = Builders<ToDoModel>.Filter.Eq("ToDoListName", TitleMainText);

                    var update = Builders<ToDoModel>.Update
                        .Set("ToDoListName", newTitle)
                        .Set("Text", newText);
                    _collectionToDo.UpdateOne(filter, update);

                    MessageBox.Show("Нагадування оновлено!");
                    break;
                case 2:
                    var fileterHomeWork = Builders<HomeWorkModel>.Filter.Eq("Name", TitleMainHomeWork);
                    var updateHomeWork = Builders<HomeWorkModel>.Update
                        .Set("Назва", newTitle)
                        .Set("Опис", newText);
                    _collectionHomeWork.UpdateOne(fileterHomeWork, updateHomeWork);
                    break;
            }
        }
        public void DeleteDataBase(int NumberFunction)
        {
            NoConnect();
            switch (NumberFunction)
            {
                case 1:
                    var filter = Builders<ToDoModel>.Filter.Eq("ToDoListName", TitleMainText);
                    _collectionToDo.DeleteOne(filter);
                    MessageBox.Show("Нагадування видалено!");
                    break;
                case 2:
                    var filterHomeWork = Builders<HomeWorkModel>.Filter.Eq("Name", TitleMainHomeWork);
                    _collectionHomeWork.DeleteMany(filterHomeWork);
                    MessageBox.Show("Домашнє завдання видалено!");
                    break;
            }
        }

        //list
        public void AddToDoList(string newToDoText, string newMainToDoListName, string IsCodeDataBase = "")
        {

            NoConnect();
            ViewContentToDoList();

            var todo = new ToDoModel
            {
                ToDoListName = newMainToDoListName,
                Text = newToDoText,
                IsCode = IsCodeDataBase,
            };

            _toDoModel = todo;
            ToDos.Add(todo);


            _collectionToDo.InsertOne(todo);
            MessageBox.Show("Нагадування додано!");
        }

        public List<ToDoModel> ViewContentToDoList()
        {
            NoConnect();

            var docuuments = _collectionToDo.Find(_ => true).ToList();

            foreach (var item in docuuments)
            {

                if (IdToDo == 0)
                    IdToDo = 1;
            }
            return docuuments;
        }

        //HomeWork
        public void AddHomeWork(string Title, string DescriptionText, string DeadLineText, string TimeDataBase)
        {
            NoConnect();

            var AddHomeWork = new HomeWorkModel
            {
                Name = Title,
                Description = DescriptionText,
                DeadLine = DeadLineText,
                Time = TimeDataBase
            };
            _collectionHomeWork.InsertOne(AddHomeWork);
            MessageBox.Show("Домашнє завдання додано!");
        }
        public List<HomeWorkModel> ViewHomeWork()
        {
            NoConnect();
            var docuuments = _collectionHomeWork.Find(_ => true).ToList();
            foreach (var item in docuuments)
            {
                if (IdToDo == 0)
                    IdToDo = 1;
            }
            return docuuments;
        }

        //Pomodoro
        public void AddTimePomodoro(string WorkTime, string ShortBreakTime, string LongBraakTime)
        {
            NoConnect();

            var AddTimePomodoro = new TimePomodoroModel
            {

                IdSettings = "Налаштування",
                ShortBreakTime = ShortBreakTime,
                WorkTime = WorkTime,
                LongBreakTime = LongBraakTime,

            };
            _collectionTimePomodoro.InsertOne(AddTimePomodoro);
        }

        public List<TimePomodoroModel> ViewTimePomodoro()
        {
            NoConnect();
            var docuuments = _collectionTimePomodoro.Find(_ => true).ToList();
            return docuuments;
        }

        public void addImage(byte[] ImageData, string Name)
        {
            var img = new ImageDocument
            {
                Name = System.IO.Path.GetFileName(Name),
                ImageData = ImageData
            };
            _collectionImagePomodoro.InsertOne(img);
            MessageBox.Show("Зображення додано!");
        }

        public List<ImageDocument> ViewImagePomodoro()
        {
            NoConnect();
            var document = _collectionImagePomodoro.Find(Builders<ImageDocument>.Filter.Empty).ToList();
            return document;
        }

        public void UpgradeSettingsPomodoro(string UpdateWorkTime, string UpdateShortBreakTime, string UpdateLongBreakTime)
        {
            NoConnect();

            var filter = Builders<TimePomodoroModel>.Filter.Eq("IdSettings", "Налаштування");
            var update = Builders<TimePomodoroModel>.Update
                .Set("WorkTime", UpdateWorkTime)
                .Set("ShortBreakTime", UpdateShortBreakTime)
                .Set("LongBreakTime", UpdateLongBreakTime);

            _collectionTimePomodoro.UpdateOne(filter, update);
        }
        public void AddColorPomodoro(string Color)
        {
            NoConnect();

            _SaveColorsSetings.DeleteMany(Builders<BsonDocument>.Filter.Empty);

            var addColors = new BsonDocument
            {
                { "Name", "Color" },
                { "ColorData", Color }
            };

            _SaveColorsSetings.InsertOne(addColors);
        }

        public List<BsonDocument> ViewColorPomodoro()
        {
            NoConnect();
            var dociuments = _SaveColorsSetings.Find(new BsonDocument()).ToList();
            return dociuments;
        }
    }
}
