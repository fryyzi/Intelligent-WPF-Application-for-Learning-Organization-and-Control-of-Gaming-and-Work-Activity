using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wizzy.Pages.DataBase.Model
{
    public class HomeWorkModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeadLine { get; set; }
        public string Time { get; set; }
    }
}
