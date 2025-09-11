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

        public void Audit(string Number)
        {

            var itemMethod = MassComboBox.SelectedItem as string;
            {
                 var indexMethod = Array.IndexOf(MassComboBox.ItemsSource.Cast<string>().ToArray(), itemMethod);
            }

            if (Number != "0")
            {
                
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
                MessageBox.Show(index.ToString());
                if (!int.TryParse(NumberCode, out int number))
                {
                    MessageBox.Show("Не вірне значення!");
                    return; 
                }
                MassConvert mass = new MassConvert();
                mass.Unit = index;
                

                switch (index)
                {
                    case 0:
                        KilogramsCode = NumberCode.ToString();
                        mass.Kilograms = number;
                        //MessageBox.Show(NumberCode);
                        mass.MassConvertMethod();

                        if (mass.Tons.ToString() != "0")
                        {
                            TonyTextBlock.Text = mass.Tons.ToString();
                        }
                        else
                        {
                            
                        }

                            GramsTextBlock.Text = mass.Grams.ToString();
                        MiligramsTextBlock.Text = mass.Milligrams.ToString();
                        TonyTextBlock.Text = mass.Tons.ToString();


                        break;
                    case 1:
                        GramsCode = NumberCode.ToString();
                        mass.Grams = int.Parse(GramsCode);

                        MiligramsTextBlock.Text = mass.Milligrams.ToString();
                        TonyTextBlock.Text = mass.Tons.ToString();
                        KilogramsTextBlock.Text = mass.Kilograms.ToString();
                        break;
                    case 2:
                        MilligramsCode = NumberCode.ToString();
                        mass.Milligrams = int.Parse(MilligramsCode);

                        GramsTextBlock.Text = mass.Grams.ToString();
                        TonyTextBlock.Text = mass.Tons.ToString();
                        KilogramsTextBlock.Text = mass.Kilograms.ToString();
                        break;
                    case 3:
                        TonsCode = NumberCode.ToString();
                        mass.Tons = int.Parse(TonsCode);

                        KilogramsTextBlock.Text = mass.Kilograms.ToString();
                        GramsTextBlock.Text = mass.Grams.ToString();
                        MiligramsTextBlock.Text = mass.Milligrams.ToString();
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
