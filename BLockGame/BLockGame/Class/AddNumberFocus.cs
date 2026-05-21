using BLockGame.FocusFolder.UltraFocus;

using MongoDB.Bson;
using MongoDB.Driver;

namespace BLockGame.Class
{
    public class AddNumberFocus
    {
        public static string FindUltraFocus { get; set; }
        public static string Pomodoro { get; set; }
        public static string FlowtimeWindow { get; set; }
        public static string Productivity_Formula { get; set; }



        private static readonly IMongoCollection<BsonDocument> _collectionProfile;


        static AddNumberFocus()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");
            _collectionProfile = database.GetCollection<BsonDocument>("Profile");
        }



        public static void UpdateUltraFocus()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", Base_User.User);
            var update = Builders<BsonDocument>.Update.Inc("UltraFocus", 1);
            _collectionProfile.UpdateOne(filter, update);
        }
        public static void UpdatePomodoro()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", Base_User.User);
            var update = Builders<BsonDocument>.Update.Inc("Pomodoro", 1);
            _collectionProfile.UpdateOne(filter, update);
        }
        public static void UpdateFlowtimeWindow()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", Base_User.User);
            var update = Builders<BsonDocument>.Update.Inc("FlowtimeWindow", 1);
            _collectionProfile.UpdateOne(filter, update);
        }
        public static void UpdateProductivity_Formula()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", Base_User.User);
            var update = Builders<BsonDocument>.Update.Inc("Productivity_Formula", 1);
            _collectionProfile.UpdateOne(filter, update);
        }


        public static void GetFocus()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", Base_User.User);
            var userProfile = _collectionProfile.Find(filter).FirstOrDefault();

            if (userProfile != null)
            {
                FindUltraFocus = userProfile.Contains("UltraFocus") ? userProfile["UltraFocus"].ToString() : "0";
                Pomodoro = userProfile.Contains("Pomodoro") ? userProfile["Pomodoro"].ToString() : "0";
                FlowtimeWindow = userProfile.Contains("FlowtimeWindow") ? userProfile["FlowtimeWindow"].ToString() : "0";
                Productivity_Formula = userProfile.Contains("Productivity_Formula") ? userProfile["Productivity_Formula"].ToString() : "0";
            }
            else
            {
                FindUltraFocus = "0";
                Pomodoro = "0";
                FlowtimeWindow = "0";
                Productivity_Formula = "0";
            }

        }

    }
}