using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string TitleMAainText;

        public static int Id = 0;

        private IMongoCollection<BsonDocument> _collection;


        public void Connect()
        {
            var client = new MongoClient("mongodb+srv://literyourutar_db_user:tbrha7ND765QQjTo@cluster0.slgjsvv.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            var batabase = client.GetDatabase("Wizzy");
            _collection = batabase.GetCollection<BsonDocument>("ToDoLists");
        }
        public void NoConnect()
        {
            if (_collection == null)
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
            _collection.InsertOne(AddToDoList);
            MessageBox.Show("Нагадування додано!");
        }

        public List<BsonDocument> ViewContentToDoList()
        {
            NoConnect();

            var dociuments = _collection.Find(new BsonDocument()).ToList();

            foreach (var item in dociuments)
            {
                Id = item.GetValue("Id").AsInt32;
                if (Id == 0)
                    Id = 1;

            }

            return dociuments;
        }
    }
}
