using System.Net.Http;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using WpfMessageBox = System.Windows.MessageBox;
using System.IO;
using MongoDB.Driver;
using MongoDB.Bson;

namespace BLockGame
{
    public partial class Team_Block : Window
    {
        private string[] Day = { "Понеділок", "Вівторок", "Середа", "Четверг", "П'ятниця", "Суббота", "Неділя" };

        private static IMongoCollection<BsonDocument> _collection;

        string apiKey = "1744EA18CBBFDCF855948A249C398F30";
        string steamId = "76561198961054657";

        // ⚠ краще перевірити щоб шлях був правильний
        private string GameDirectory = @"D:\Game Programs\Steam\steamapps\";

        private static string ItemComboBox;
        private static string StartText;
        private static string EndText;
        private static string FileName;
        private static string foldername;

        private List<string> gameNames = new();

        public Team_Block()
        {
            InitializeComponent();

            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("BLockGame");
            _collection = database.GetCollection<BsonDocument>("BlockUserGame");

            foreach (var day in Day)
                DayComboBox.Items.Add(day);

            _ = LoadGamesAsync();
        }

        // -------------------- LOAD GAMES --------------------

        private async Task LoadGamesAsync()
        {
            string url = $"https://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={steamId}&include_appinfo=1";

            using HttpClient client = new();
            var response = await client.GetAsync(url);

            string json = await response.Content.ReadAsStringAsync();
            JObject data = JObject.Parse(json);

            var games = data["response"]?["games"];

            if (games == null) return;

            gameNames.Clear();

            foreach (var game in games)
            {
                string name = game["name"]?.ToString();
                if (!string.IsNullOrWhiteSpace(name))
                    gameNames.Add(name);
            }

            ListSteamGame.ItemsSource = gameNames;
        }

        // -------------------- FIXED SEARCH EXE --------------------

        private async Task<string> FindGameExecutableAsync(string gameName)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (!Directory.Exists(GameDirectory))
                        return null;

                    var dirs = Directory.GetDirectories(GameDirectory);

                    foreach (var dir in dirs)
                    {
                        var exeFiles = Directory.GetFiles(dir, "*.exe", SearchOption.AllDirectories);

                        foreach (var exe in exeFiles)
                        {
                            string exeName = Path.GetFileNameWithoutExtension(exe);

                            if (exeName != null &&
                                exeName.Contains(gameName, StringComparison.OrdinalIgnoreCase))
                            {
                                return exe;
                            }
                        }
                    }
                }
                catch
                {
                    return null;
                }

                return null;
            });
        }

        // -------------------- SEARCH FILTER --------------------

        private void SearchGame_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = SearchGame.Text?.ToLower() ?? "";

            ListSteamGame.ItemsSource = gameNames
                .Where(g => g.ToLower().Contains(text))
                .ToList();
        }

        // -------------------- SAVE BUTTON --------------------

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DayComboBox.SelectedItem == null)
            {
                WpfMessageBox.Show("Виберіть день!");
                return;
            }

            if (string.IsNullOrWhiteSpace(MinDay.Text) ||
                string.IsNullOrWhiteSpace(MaxDay.Text))
            {
                WpfMessageBox.Show("Введіть час!");
                return;
            }

            if (string.IsNullOrWhiteSpace(FileName) ||
                string.IsNullOrWhiteSpace(foldername) ||
                string.IsNullOrWhiteSpace(Base_User.User))
            {
                WpfMessageBox.Show("Не вибрана гра або користувач!");
                return;
            }

            ItemComboBox = DayComboBox.SelectedItem.ToString();
            StartText = MinDay.Text;
            EndText = MaxDay.Text;

            var doc = new BsonDocument
            {
                { "Id_Game", FileName },
                { "Name_Game", foldername },
                { "User", Base_User.User },
                { "Day", ItemComboBox },
                { "StartTime", StartText },
                { "EndTime", EndText }
            };

            try
            {
                _collection.InsertOne(doc);
                WpfMessageBox.Show("Збережено!");
            }
            catch (Exception ex)
            {
                WpfMessageBox.Show(ex.Message);
            }
        }

        // -------------------- SELECT GAME --------------------

        private async void ListSteamGame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListSteamGame.SelectedItem is not string selectedGame)
                return;

            selectedGame = selectedGame.Trim();

            string foundPath = await FindGameExecutableAsync(selectedGame);

            if (!string.IsNullOrWhiteSpace(foundPath))
            {
                foldername = foundPath;
                FileName = Path.GetFileName(foundPath);

                System.Windows.Clipboard.SetText(FileName);

                WpfMessageBox.Show($"Знайдено: {FileName}");
            }
            else
            {
                WpfMessageBox.Show("Файл гри не знайдено");
            }
        }

        private void ListSteamGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}