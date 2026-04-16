using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Wizzy.Pages.DataBase.Model
{
    public class AddImageModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        public byte[] ImageData { get; set; }
    }
}
