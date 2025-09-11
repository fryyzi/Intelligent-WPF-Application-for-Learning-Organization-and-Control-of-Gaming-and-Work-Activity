using NAudio.CoreAudioApi;
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

namespace Wizzy.Pages.Tools.AllTools.Converns.Mass
{
    /// <summary>
    /// Логика взаимодействия для Mass.xaml
    /// </summary>
    public partial class Mass : UserControl
    {
        MassConvert mass = new MassConvert();
        public Mass()
        {
            InitializeComponent();

            MassComboBox.ItemsSource = new List<string>
            {
                "Кілограми(КГ)",
                "Грами(Г)",
                "Міліграми(МГ)",
                "Тони(Т)",
            };
            MassComboBox.SelectedIndex = 0;
        }

        public void Audit()
        {
            mass.MassConvertMethod();

            var itemMethod = MassComboBox.SelectedItem as string;
            {
                 var indexMethod = Array.IndexOf(MassComboBox.ItemsSource.Cast<string>().ToArray(), itemMethod);
            }
 
            if (mass.Grams.ToString() != "0")
            {
                GramsTextBlock.Text = $"Грамів: {mass.Grams.ToString()}";
            }
            else
            {
                GramsTextBlock.Text = $" ";
            }
            if (mass.Milligrams.ToString() != "0")
            {
                MiligramsTextBlock.Text = $"Міліграмів: {mass.Milligrams.ToString()}";
            }
            else
            {
                MiligramsTextBlock.Text = $" ";
            }

            if (mass.Kilograms.ToString() != "0")
            {
                KilogramsTextBlock.Text = $"Килограмів: {mass.Kilograms.ToString()}";
            }

            else
            {
                KilogramsTextBlock.Text = $" ";
            }

            if (mass.Tons.ToString() != "0")
            {
                TonyTextBlock.Text = $"Тон: {mass.Tons.ToString()}";
            }
            else
            {
                TonyTextBlock.Text = $" ";
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            var NumberCode = NumberTexBox.Text;

            var KilogramsCode = String.Empty;
            var GramsCode = String.Empty;
            var MilligramsCode = String.Empty;
            var TonsCode = String.Empty;


            var item = MassComboBox.SelectedItem as string;
            {
                var index = Array.IndexOf(MassComboBox.ItemsSource.Cast<string>().ToArray(), item);
                //MessageBox.Show(index.ToString());
                if (!int.TryParse(NumberCode, out int number))
                {
                    MessageBox.Show("Не вірне значення!");
                    return; 
                }
                
                mass.Unit = index;

                switch (index)
                {
                    case 0:
                        mass.Kilograms = number;
                        Audit();
                        break;
                    case 1:
                        mass.Grams = number;
                        Audit();
                        break;
                    case 2:
                        mass.Milligrams = number;
                        Audit();
                        break;
                    case 3:
                        mass.Tons = number;
                        Audit();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
