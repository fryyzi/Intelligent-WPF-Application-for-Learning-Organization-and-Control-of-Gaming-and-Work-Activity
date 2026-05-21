using BLockGame.Admin;
using BLockGame.Class;
using BLockGame.FocusFolder;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using WpfApp = System.Windows.Application;

namespace BLockGame
{
    public partial class Main_Manu : Window
    {
        private NotifyIcon trayIcon;
        private BlockApp _blockApp;

        private readonly string FileCode = "3122312d2-odm0if3jf3ur0fjmrr033rc";
        private string FileText = string.Empty;

        public Main_Manu()
        {
            InitializeComponent();

            SetupTray();
            CheckLicenseFile();

            _blockApp = new BlockApp();

            // Перевіряємо початковий стан галочки під час запуску вікна

            _blockApp.StartMonitoring(Base_User.User);
        }

        // Спрацьовує, коли користувач ставить галочку "Блокувати програми"
        private void BlockAppsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _blockApp?.StartMonitoring(Base_User.User);
        }

        // Спрацьовує, коли користувач прибирає галочку
        private void BlockAppsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _blockApp?.StopMonitoring();
        }

        private void SetupTray()
        {
            if (!Base_User.IsAutotization)
                return;

            trayIcon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.Application, 40, 40),
                Text = "BlockGame",
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip()
            };

            trayIcon.ContextMenuStrip.Items.Add("Відкрити", null, OpenApp);
            trayIcon.ContextMenuStrip.Items.Add("Закрити", null, ExitApp);

            this.Closing += Windows_Closing;
        }

        private void CheckLicenseFile()
        {
            string filePath = @"F:\programing\Project\Block_Game\BLockGame\BLockGame\bin\Debug\net8.0-windows\Code.txt";

            if (!File.Exists(filePath))
                return;

            FileText = File.ReadAllText(filePath);

            if (FileText == FileCode)
            {
                DeleteApp.IsEnabled = true;
                DeleteApp.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void Windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();

            trayIcon?.ShowBalloonTip(
                1000,
                "Програма згорнута",
                "Програма працює у фоновому режимі",
                ToolTipIcon.Info);
        }

        private void OpenApp(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void ExitApp(object sender, EventArgs e)
        {
            _blockApp?.StopMonitoring(); // Зупиняємо моніторинг перед закриттям додатка
            trayIcon?.Dispose();
            WpfApp.Current.Shutdown();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new Window_Block_Game().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new License().Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new About().Show();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            new Profile.Profile().Show();
        }

        private void LogsAdmin(object sender, RoutedEventArgs e)
        {
            new Logi().Show();
        }

        private void FocusModes_Click(object sender, RoutedEventArgs e)
        {
            new FocusFolder.MainWindowFocus.All_Focus().Show();
        }

        private void Notes_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}