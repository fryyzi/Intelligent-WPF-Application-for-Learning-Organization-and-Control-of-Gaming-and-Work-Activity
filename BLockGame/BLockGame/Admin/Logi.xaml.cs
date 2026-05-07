using BLockGame.Model;
using Microsoft.VisualBasic.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BLockGame.Admin
{
    /// <summary>
    /// Interaction logic for Logi.xaml
    /// </summary>
    public partial class Logi : Window
    {
        private static IMongoCollection<BsonDocument> _collection;




        public Logi()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");

            _collection = database.GetCollection<BsonDocument>("BlockUserGame");
            InitializeComponent();
            LoadLogs();
        }

        public void LoadLogs()
        {

            var logs = new List<LogsModel>();

            var logsData = _collection.Find(new BsonDocument()).ToList();

            foreach (var item in logsData)
            {
                var User = item["User"].ToString();
                var ProgramName = item["Id_Game"].ToString();
                var Day = item["Day"].ToString();
                var StartTime = item["StartTime"].ToString();
                var EtartTime = item["EndTime"].ToString();
                var StartTimeBlockProgram = item["StartTimeBLockPrograms"].ToString();



                logs.Add(new LogsModel
                {
                    Name = User,
                    ProgramName = ProgramName,
                    Day = Day,
                    StartTime = StartTime,
                    EndTime = EtartTime,
                    StartTimeBLockPrograms = StartTimeBlockProgram
                });
            }
            MyGrid.ItemsSource = logs;
        }
    }
}
