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

        public static void UpdateFocus()
        {

        }

    }
}