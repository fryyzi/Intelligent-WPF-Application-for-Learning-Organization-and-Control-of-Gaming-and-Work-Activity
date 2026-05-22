using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLockGame.Model
{
    public class ProfileModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Login { get; set; }
        public string RegisterNickName { get; set; }
        public string Email { get; set; }
        public string UserDescription { get; set; }
        public int UltraFocus { get; set; }
        public int Pomodoro { get; set; }
        public int Flowtime { get; set; }
        public int Productivity_Formula { get; set; }

    }
}
