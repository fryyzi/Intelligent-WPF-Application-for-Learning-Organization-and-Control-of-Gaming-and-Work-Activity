using BLockGame.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows;

namespace BLockGame.Profile
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {

        public static string FindNameProgramDataBase;
        public static string FindUserDescription;

        private static IMongoCollection<BsonDocument> _collectionProfile;

        public Profile()
        {
            InitializeComponent();

            ViewProfile.ViewProfileData();
            AddNumberFocus.GetFocus();

            UserNickname.Text = ViewProfile.FindNameProgramDataBase;
            UserDescription.Text = ViewProfile.FindUserDescription;

            CountPomodoro.Text = AddNumberFocus.Pomodoro;
            CountFlowtime.Text = AddNumberFocus.FlowtimeWindow;
            CountWaveFlow.Text = AddNumberFocus.Productivity_Formula;
            CountUltraFocus.Text = AddNumberFocus.FindUltraFocus;


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new ProfileSettingsWindow().ShowDialog();
        }
    }
}
