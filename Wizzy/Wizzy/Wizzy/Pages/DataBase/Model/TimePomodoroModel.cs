using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Wizzy.Pages.DataBase.Model
{
    public class TimePomodoroModel
    {
        [BsonId]
        [BsonElement("Id")]
        public int ObjectId { get; set; }
        public string IdSettings { get; set; }
        public string WorkTime { get; set; }
        public string ShortBreakTime { get; set; }
        public string LongBreakTime { get; set; }
    }
}
