using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wizzy.Pages.DataBase.Model
{
    public class ToDoModel
    {
        [BsonId]
        [BsonElement("Id")]
        public ObjectId Id { get; set; }

        public string ToDoListName { get; set; }
        public string Text { get; set; }

        [BsonElement("Код")]
        public string IsCode { get; set; }
    }
}
