using NAudio.Wave;
using System.Media;
using System.Threading;


namespace ConsoleWizzyTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var autifile = new AudioFileReader(@"F:\programing\C# Programs\Wizzy\Wizzy\Sound\Open.wav");
            autifile.Volume = 0.8f;

            var outputDevice = new WaveOutEvent();
            outputDevice.Init(autifile);
            outputDevice.Play();

            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100); // чекає 0.1 секунди в циклі
            }

        }
    }
}
