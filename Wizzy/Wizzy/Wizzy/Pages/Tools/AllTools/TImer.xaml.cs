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
        public TImer()
        {
            InitializeComponent();

            timer = new System.Timers.Timer(1);
            timer.Start();
        }


        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CodeMilSec++;

            if (CodeMilSec == 60)
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
        private void StartButtont_Click(object sender, RoutedEventArgs e)
        {
            timer.Elapsed += Timer_Elapsed;
        }
    }
}
