using MongoDB.Bson;
using MongoDB.Driver;

namespace BLockGame.Class
{
    public class ViewProfile
    {
        public static string FindNameProgramDataBase { get; set; }
        public static string FindUserDescription { get; set; }

        private static readonly IMongoCollection<BsonDocument> _collectionProfile;

        public static string Nickname;

        static ViewProfile()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");
            _collectionProfile = database.GetCollection<BsonDocument>("Profile");
        }

        public static void ViewProfileData()
        {
            if (string.IsNullOrEmpty(Base_User.User))
            {
                FindNameProgramDataBase = "Гість";
                FindUserDescription = "Опис відсутній.";
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("RegisterNickName", Base_User.User);

            var userProfile = _collectionProfile.Find(filter).FirstOrDefault();

            if (userProfile != null)
            {
                FindNameProgramDataBase = userProfile.Contains("Login") ? userProfile["Login"].ToString() : Base_User.User;
                FindUserDescription = userProfile.Contains("UserDescription") ? userProfile["UserDescription"].ToString() : "Опис відсутній.";
            }
            else
            {
                FindNameProgramDataBase = Base_User.User;
                FindUserDescription = "Неналаштований акаунт";
            }
        }

        public static void UpdateProfileData(string newNickname, string newDescription)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Login", FindNameProgramDataBase);
            var update = Builders<BsonDocument>.Update
                .Set("Login", newNickname)
                .Set("UserDescription", newDescription);
            _collectionProfile.UpdateOne(filter, update);

            FindNameProgramDataBase = newNickname;
            FindUserDescription = newDescription;
        }

    }
}