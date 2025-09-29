using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Pages.DataBase.DataBase dataBase = new Pages.DataBase.DataBase();

        string WorkTime = String.Empty;
        string ShortBreakTime = String.Empty;
        string LongBreakTime = String.Empty;

        public Settings()
        {
            InitializeComponent();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            dataBase.Connect();

            WorkTime = WorkDurationTextBox.Text.Trim();
            ShortBreakTime = ShortBreakDurationTextBox.Text.Trim();
            LongBreakTime = LongBreakDurationTextBox.Text.Trim();

            //Pages.DataBase.DataBase.WorkTimeDataBase = WorkTime;

            if (string.IsNullOrEmpty(WorkTime) || string.IsNullOrEmpty(ShortBreakTime) || string.IsNullOrEmpty(LongBreakTime))
            {
                WorkTime = "25";
                ShortBreakTime = "5";
                LongBreakTime = "15";

                dataBase.AddTimePomodoro(WorkTime, ShortBreakTime, LongBreakTime);
            }
            else
            {
                dataBase.UpgradeSettingsPomodoro(WorkTime, ShortBreakTime, LongBreakTime);
            }
            this.Visibility = Visibility.Hidden;
        }
    }
}
