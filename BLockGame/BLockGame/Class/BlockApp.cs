using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BLockGame.Class
{
    public class BlockApp
    {
        private readonly IMongoCollection<BsonDocument> _collectionGame;
        private readonly CultureInfo _culture = new CultureInfo("uk-UA");

        private string _idProgram;
        private string _nicknameUserEmail;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _day;



        public BlockApp()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");

            _collectionGame = database.GetCollection<BsonDocument>("BlockUserGame");
        }

        public void LoadData()
        {
            var gameBlock = _collectionGame.Find(new BsonDocument()).ToList();

            foreach (var item in gameBlock)
            {
                _idProgram = item["Id_Game"].ToString();
                _nicknameUserEmail = item["User"].ToString();
                _startTime = TimeSpan.Parse(item["StartTime"].ToString());
                _endTime = TimeSpan.Parse(item["EndTime"].ToString());
                _day = item["Day"].ToString();
            }
        }

        public void StartMonitoring(string currentUserEmail, int numberFocus, string mode)
        {
            LoadData();
            Task.Run(() => MonitorGameStatus(currentUserEmail, numberFocus, mode));
        }

        private void MonitorGameStatus(string currentUserEmail, int numberFocus, string mode)
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                TimeSpan currentTime = now.TimeOfDay;
                string dayOfWeekUkrainian = now.ToString("dddd", _culture);

                if (mode == "BD")
                {
                    if (currentUserEmail == _nicknameUserEmail)
                    {
                        bool isCorrectDay = dayOfWeekUkrainian.Equals(_day, StringComparison.OrdinalIgnoreCase);
                        bool isAllowedTime = currentTime >= _startTime && currentTime < _endTime;

                        if (!(isCorrectDay && isAllowedTime))
                        {
                            if (IsAppRunning(_idProgram))
                            {
                                CloseApp(numberFocus, mode);
                            }
                        }
                    }
                }
                else if (mode == "NoBD")
                {
                    CloseApp(numberFocus, mode);
                }

                Thread.Sleep(5000);
            }
        }

        private bool IsAppRunning(string exeName)
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.ProcessName.Equals(
                    Path.GetFileNameWithoutExtension(exeName),
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private void CloseApp(int numberFocus, string mode)
        {
            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    switch (mode)
                    {
                        case "BD":
                            if (process.ProcessName.Equals(
                                Path.GetFileNameWithoutExtension(_idProgram),
                                StringComparison.OrdinalIgnoreCase))
                            {
                                process.Kill();
                            }
                            break;

                        case "NoBD":
                            //if
                            switch (numberFocus)
                            {
                                case 3:
                                    string[] blockedApps = { "steam", "Telegram" };

                                    foreach (string blockedApp in blockedApps)
                                    {
                                        if (process.ProcessName.Equals(blockedApp, StringComparison.OrdinalIgnoreCase))
                                        {
                                            process.Kill();
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                catch
                {
                }
            }
        }
    }
}