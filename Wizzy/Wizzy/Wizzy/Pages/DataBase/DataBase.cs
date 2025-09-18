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

namespace Wizzy.Pages.DataBase
{
    class DataBase
    {
        public static string Text = "Not Text";
        public static string ToDoListName = "No Name ToDoList";

        public static string HomeWorkTitle = "No Name HomeWork";

        public static string TitleMAainText;

        public static int Id = 0;

        private IMongoCollection<BsonDocument> _collectionToDo;
        private IMongoCollection<BsonDocument> _collectionHomeWork;


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
        }
        public void NoConnect()
        {
            if (_collectionToDo == null || _collectionHomeWork == null)
                Connect();
        }

        public void AddToDoList(string newToDoText, string newMainToDoListName)
        {
            NoConnect();
            ViewContentToDoList();

            var AddToDoList = new BsonDocument
            {
                { "ToDoListName", newMainToDoListName },
                { "Text", newToDoText },
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
        public void UpdateToDo(string newTitle, string newText)
        {
            NoConnect();

            var filter = Builders<BsonDocument>.Filter.Eq("Text", TitleMAainText);

            var update = Builders<BsonDocument>.Update
                .Set("ToDoListName", newTitle)
                .Set("Text", newText);
            _collectionToDo.UpdateOne(filter, update);

            MessageBox.Show("Нагадування оновлено!");
        }

        public void DeleteToDo()
        {
            NoConnect();
            var filter = Builders<BsonDocument>.Filter.Eq("Text", TitleMAainText);
            _collectionToDo.DeleteOne(filter);
            MessageBox.Show("Нагадування видалено!");
            
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
                if (Id == 0)
                    Id = 1;
            }
            return dociuments;
        }
    }
}
