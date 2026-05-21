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

        // Джерело токенів для безпечної зупинки фонового потоку
        private CancellationTokenSource _cts;

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
                _idProgram = item.Contains("Id_Game") ? item["Id_Game"].ToString() : string.Empty;
                _nicknameUserEmail = item.Contains("User") ? item["User"].ToString() : string.Empty;
                _startTime = item.Contains("StartTime") ? TimeSpan.Parse(item["StartTime"].ToString()) : TimeSpan.Zero;
                _endTime = item.Contains("EndTime") ? TimeSpan.Parse(item["EndTime"].ToString()) : TimeSpan.Zero;
                _day = item.Contains("Day") ? item["Day"].ToString() : string.Empty;
            }
        }

        public void StartMonitoring(string currentUserEmail)
        {
            StartMonitoring(currentUserEmail, 0, "BD");
        }

        public void StartMonitoring(string currentUserEmail, int numberFocus, string mode)
        {
            StopMonitoring();

            _cts = new CancellationTokenSource();
            LoadData();

            CancellationToken token = _cts.Token;
            Task.Run(() => MonitorGameStatus(currentUserEmail, numberFocus, mode, token), token);
        }

        public void StopMonitoring()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void MonitorGameStatus(string currentUserEmail, int numberFocus, string mode, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                TimeSpan currentTime = now.TimeOfDay;
                string dayOfWeekUkrainian = now.ToString("dddd", _culture);

                if (mode == "BD")
                {
                    if (!string.IsNullOrEmpty(_nicknameUserEmail) && currentUserEmail == _nicknameUserEmail)
                    {
                        bool isCorrectDay = dayOfWeekUkrainian.Equals(_day, StringComparison.OrdinalIgnoreCase);
                        bool isAllowedTime = currentTime >= _startTime && currentTime < _endTime;

                        if (!(isCorrectDay && isAllowedTime))
                        {
                            if (!string.IsNullOrEmpty(_idProgram) && IsAppRunning(_idProgram))
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

                try
                {
                    Task.Delay(5000, token).Wait(token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private bool IsAppRunning(string exeName)
        {
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.ProcessName.Equals(Path.GetFileNameWithoutExtension(exeName), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                catch { }
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
                            if (process.ProcessName.Equals(Path.GetFileNameWithoutExtension(_idProgram), StringComparison.OrdinalIgnoreCase))
                            {
                                process.Kill();
                            }
                            break;

                        case "NoBD":
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
                catch { }
            }
        }
    }
}