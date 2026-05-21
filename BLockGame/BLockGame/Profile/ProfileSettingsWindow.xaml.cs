using BLockGame.Class;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Windows;

using WpfMessageBox = System.Windows.MessageBox;

namespace BLockGame.Profile
{
    /// <summary>
    /// Interaction logic for ProfileSettingsWindow.xaml
    /// </summary>
    public partial class ProfileSettingsWindow : Window
    {
        private readonly IMongoCollection<BsonDocument> _collectionProfile;


        public static string FindNameProgramDataBase;
        public static string FindUserDescription;

        public static string EditNicknameText;
        public static string EditDescriptionText;



        public ProfileSettingsWindow()
        {
            InitializeComponent();


            ViewProfile.ViewProfileData();

            EditNickname.Text = ViewProfile.FindNameProgramDataBase;
            EditDescription.Text = ViewProfile.FindUserDescription;

            ViewNickName.Text = ViewProfile.FindNameProgramDataBase;
            ViewNickDecription.Text = ViewProfile.FindUserDescription;
        }
        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            EditNicknameText = EditNickname.Text;
            EditDescriptionText = EditDescription.Text;

            WpfMessageBox.Show(EditDescriptionText);

            ViewProfile.UpdateProfileData(EditNicknameText, EditDescriptionText);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
