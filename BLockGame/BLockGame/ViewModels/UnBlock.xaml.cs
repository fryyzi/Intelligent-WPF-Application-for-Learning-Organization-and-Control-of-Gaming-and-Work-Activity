using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows;

using WpfMessageBox = System.Windows.MessageBox;

namespace BLockGame
{
    public partial class UnBlock : Window
    {

        private static IMongoCollection<BsonDocument> _collection;

        private static string FindNameProgramDataBase;
        private static string DeleteTextBox;
        public UnBlock()
        {
            InitializeComponent();


            //var client = new MongoClient("mongodb+srv://fryyzihnk:TgBtqkuh70KbK6JX@clusteruser.rhkqo.mongodb.net/BLockGame?retryWrites=true&w=majority&appName=ClusterUser");
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");

            _collection = database.GetCollection<BsonDocument>("BlockUserGame");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var NameProgram = _collection.Find(new BsonDocument()).ToList();

            foreach (var item in NameProgram)
            {
                FindNameProgramDataBase = item["Id_Game"].ToString();
                DataTextBox.Text = FindNameProgramDataBase;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DeleteTextBox))
            {
                WpfMessageBox.Show("Ви не ввели назву програми яку хочете видалити!");
            }
            else
            {
                DeleteTextBox = DeteleAppTextBox.Text;
                var filter = Builders<BsonDocument>.Filter.Eq("Id_Game", DeleteTextBox);

                _collection.DeleteOne(filter);
            }
        }
    }
}

