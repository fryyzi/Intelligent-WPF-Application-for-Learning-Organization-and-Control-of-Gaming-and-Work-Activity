using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
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

        public static int Id = 0;
        public static int IsCodeDataBase;

        public static string WorkTimeDataBase = "";

        private IMongoCollection<BsonDocument> _collectionToDo;
        private IMongoCollection<BsonDocument> _collectionHomeWork;
        private IMongoCollection<BsonDocument> _collectionTimePomodoro;
        private IMongoCollection<ImageDocument> _collectionImagePomodoro;


        public void Connect()
        {
            var settings = MongoClientSettings.FromConnectionString(
                "mongodb://localhost:27017/"
            );
            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };

            var client = new MongoClient(settings);
            var database = client.GetDatabase("Wizzy");
            _collectionToDo = database.GetCollection<BsonDocument>("ToDoLists");
            _collectionHomeWork = database.GetCollection<BsonDocument>("HomeWork");
            _collectionTimePomodoro = database.GetCollection<BsonDocument>("Time");
            _collectionImagePomodoro = database.GetCollection<ImageDocument>("SaveSetings");
        }
        public void NoConnect()
        {
            if (_collectionToDo == null || _collectionHomeWork == null || _collectionTimePomodoro == null)
                Connect();
        }

        public void AddToDoList(string newToDoText, string newMainToDoListName, string IsCodeDataBase = "")
        {
            NoConnect();
            ViewContentToDoList();

            var AddToDoList = new BsonDocument
            {
                { "ToDoListName", newMainToDoListName },
                { "Text", newToDoText },
                {"Код", IsCodeDataBase},
                //{"IsCode", CodeFunction},
                { "Id", Id += 1 }
            };
            _collectionToDo.InsertOne(AddToDoList);
            MessageBox.Show("Нагадування додано!"); 
        }

        public List<BsonDocument> ViewContentToDoList()

        {
            NoConnect();

            var dociuments = _collectionToDo.Find(new BsonDocument()).ToList();

            foreach (var item in dociuments)
            {
                Id = item.GetValue("Id").AsInt32;
                if (Id == 0)
                    Id = 1;
            }
            return dociuments;
        }

        public void UpdateDataBase(string newTitle, string newText, int NumberFunction)
        {
            NoConnect();

            switch (NumberFunction)
            {
                case 1:
                    var filter = Builders<BsonDocument>.Filter.Eq("ToDoListName", TitleMainText);

                    var update = Builders<BsonDocument>.Update
                        .Set("ToDoListName", newTitle)
                        .Set("Text", newText);
                    _collectionToDo.UpdateOne(filter, update);

                    MessageBox.Show("Нагадування оновлено!");
                    break;
                case 2:
                    var fileterHomeWork = Builders<BsonDocument>.Filter.Eq("Назва", TitleMainHomeWork);
                    var updateHomeWork = Builders<BsonDocument>.Update
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
                    var filter = Builders<BsonDocument>.Filter.Eq("ToDoListName", TitleMainText);
                    _collectionToDo.DeleteOne(filter);
                    MessageBox.Show("Нагадування видалено!");
                    break;
                case 2:
                    var filterHomeWork = Builders<BsonDocument>.Filter.Eq("Назва", TitleMainHomeWork);
                    _collectionHomeWork.DeleteMany(filterHomeWork);
                    MessageBox.Show("Домашнє завдання видалено!");
                    break;
            }
        }

        public void AddHomeWork(string Title, string DescriptionText)
        {
            NoConnect();
            
            var AddHomeWork = new BsonDocument
            {
                {"Назва", Title},
                {"Опис", DescriptionText},
                {"id", Id += 1}
            };
            _collectionHomeWork.InsertOne(AddHomeWork);
            MessageBox.Show("Домашнє завдання додано!");
        }
        public List<BsonDocument> ViewHomeWork()
        {
            NoConnect();
            var dociuments = _collectionHomeWork.Find(new BsonDocument()).ToList();
            foreach (var item in dociuments)
            {
                Id = item.GetValue("id").AsInt32;
                IsCodeDataBase = item.GetValue("Код").AsInt32;
                if (Id == 0)
                    Id = 1;

            }
            return dociuments;
        }
    
        public void AddTimePomodoro(string WorkTime, string ShortBreakTime, string LongBraakTime)
        {
            NoConnect();

            var AddTimePomodoro = new BsonDocument
            {
                {"IdSettings", "Налаштування"},
                {"WorkTime", WorkTime},
                {"ShortBreakTime", ShortBreakTime},
                {"LongBreakTime", LongBraakTime}
                
            };
            _collectionTimePomodoro.InsertOne(AddTimePomodoro);
        }
    
        public List<BsonDocument> ViewTimePomodoro()
        {
            NoConnect();
            var dociuments = _collectionTimePomodoro.Find(new BsonDocument()).ToList();
            return dociuments;
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

            var filter = Builders<BsonDocument>.Filter.Eq("IdSettings", "Налаштування");
            var update = Builders<BsonDocument>.Update
                .Set("WorkTime", UpdateWorkTime)
                .Set("ShortBreakTime", UpdateShortBreakTime)
                .Set("LongBreakTime", UpdateLongBreakTime);

            _collectionTimePomodoro.UpdateOne(filter, update);
        }
    }
}
