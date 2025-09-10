using System.Timers;
using System.Windows;
using System.Threading;


namespace Wizzy.Pages.Tools.AllTools
{
    /// <summary>
    /// Логика взаимодействия для TImer.xaml
    /// </summary>
    public partial class TImer : Window
    {
        System.Timers.Timer timer;


        int CodeSec = 0;
        int CodeMilSec = 0;

        int SaveSec = 0;
        int SaveMilSec = 0;
        public TImer()
        {
            InitializeComponent();

            timer = new System.Timers.Timer(1);
            timer.Start();
        }


        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CodeMilSec++;

            if (CodeMilSec == 60) // бо 1000 мс = 1 секунда
            {
                CodeSec++;
                CodeMilSec = 0;
            }

            Dispatcher.Invoke(() =>
            {
                Sec.Content = CodeSec.ToString();
                MiliSec.Content = CodeMilSec.ToString();
            });
        }

        private void StartButton(object? sender, ElapsedEventArgs e)
        {
            CodeSec = SaveSec;
            CodeMilSec = SaveMilSec;
            Dispatcher.Invoke(() =>
            {
                Sec.Content = CodeSec.ToString();
                MiliSec.Content = CodeMilSec.ToString();
                timer.Start();
            });
            
        }

        private void StartButtont_Click(object sender, RoutedEventArgs e)
        {
            timer.Elapsed += Timer_Elapsed;
        }

        private void StopButtont_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            SaveSec = CodeSec;
            SaveMilSec = CodeMilSec;
        }

        private void ResumeButtont_Click(object sender, RoutedEventArgs e)
        {
            CodeSec = SaveSec;
            CodeMilSec = SaveMilSec;
            timer.Start();
        }
    }
}
