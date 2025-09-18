using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.ComponentModel.Design;
using System.Security.Authentication;
using System.Timers;

class Program
{

    static void Main()
    {

        var client = new MongoClient("mongodb://localhost:27017/");
        var database = client.GetDatabase("Wizzy");
        var _collection = database.GetCollection<BsonDocument>("ToDoLists");

        var AddToDoList = new BsonDocument
            {
                { "ToDoListName", "qweqwe" },
                { "Text", "eqweqw" },
            };
        _collection.InsertOne(AddToDoList);
        Console.WriteLine("Нагадування додано!");


    }

    
}
