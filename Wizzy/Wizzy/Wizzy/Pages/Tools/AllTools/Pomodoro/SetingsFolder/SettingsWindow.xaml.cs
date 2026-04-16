using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using Wizzy.Pages.Tools.AllTools.Pomodoro.Setings;

namespace Wizzy.Pages.Tools.AllTools.Pomodoro.SetingsFolder
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void TimerSettings_Click(object sender, RoutedEventArgs e)
        {
            ControlSetting.Content = new SettingTIme();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            BackGroundSetting settingsWindow = new BackGroundSetting();
            settingsWindow.Show();
            this.Close();
        }

        private void Button_Click_Colors(object sender, RoutedEventArgs e)
        {
            ControlSetting.Content = new AllSetings.ColorSettings();

        }
    }
}
