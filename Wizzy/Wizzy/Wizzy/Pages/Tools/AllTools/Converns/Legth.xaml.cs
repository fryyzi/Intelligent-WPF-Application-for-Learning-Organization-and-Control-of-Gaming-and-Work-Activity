using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
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

namespace Wizzy.Pages.Tools.AllTools.Converns
{
    /// <summary>
    /// Логика взаимодействия для Legth.xaml
    /// </summary>
    public partial class Legth : UserControl
    {
        public Legth()
        {
            InitializeComponent();
            LeghtComboBox.ItemsSource = new List<String>
            {
                "Метри(М)",
                "Міліметри(ММ)",
                "Километри(КМ)",
                "Сантиметри(СМ)",
                "Милі",
                "Ядра",
                "Фути",
                "Дюйми",
            };
            LeghtComboBox.SelectedIndex = 0;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ConvertLeght convertLeght = new ConvertLeght();
            string NumberCode = NumberTexBox.Text;
            string NumberConvertCode = String.Empty;

            string Wight = String.Empty;

            var item = LeghtComboBox.SelectedItem as string;
            if (item != null)
            {
                int index = Array.IndexOf(LeghtComboBox.ItemsSource.Cast<string>().ToArray(), item);

                
                convertLeght.Unit = index;

                if (!int.TryParse(NumberCode, out int number) || NumberCode == "0")
                {
                    MessageBox.Show("Не вірне значення!");
                    return;
                }

                convertLeght.Meters = number;
                convertLeght.ConvertMethod();
                
            }
            NumberConvertCode = convertLeght.Kilometers.ToString();;
            if(NumberConvertCode.ToString() != "0")
            {
                KilometersTextBlock.Text = $"Километрів: {NumberConvertCode.ToString()}";
            }
            else
            {
                KilometersTextBlock.Text = " ";
            }

            if(convertLeght.Centimeters.ToString() != "0")
            {
               CentimetersTextBlock.Text = $"Сантиметрів: {convertLeght.Centimeters.ToString()}";
            }
            else
            {
                CentimetersTextBlock.Text = " ";
            }

            if (convertLeght.Millimeters.ToString() != "0")
            {
                MillimetersTextBlock.Text = $"Міліметрів: {convertLeght.Millimeters.ToString()}";
            }

            else
            {
                MillimetersTextBlock.Text = " ";
            }

            if(convertLeght.Decimeters.ToString() != "0") 
            {
                DecitemersTextBLock.Text = $"Дециметрів: {convertLeght.Decimeters.ToString()}";
            }
            else
            {
                DecitemersTextBLock.Text = " ";
            }

            if (convertLeght.Micrometers.ToString() != "0")
            {
                NanometersTextBLock.Text = $"Мікрометрів: {convertLeght.Micrometers.ToString()}";
            }
            else
            {
                NanometersTextBLock.Text = " ";
            }

            if (convertLeght.Nanometers.ToString() != "0")
            {
                MilesTextBLock.Text = $"Нанометри: {convertLeght.Nanometers.ToString()}";
            }
            else
            {
                MillimetersTextBlock.Text = " ";
            }

            if (convertLeght.Miles.ToString() != "0")
            {
                YardsTextBlock.Text = $"Миль: {convertLeght.Miles.ToString()}";
            }
            else
            {
                YardsTextBlock.Text = " ";
            }

            if (convertLeght.Feet.ToString() != "0")
            {
                FeetTextBlock.Text = $"Миль: {convertLeght.Feet.ToString()}";
            }
            else
            {
                FeetTextBlock.Text = " ";
            }

            if (convertLeght.Inches.ToString() != "0")
            {
                InchesTextBlock.Text = $"Миль: {convertLeght.Inches.ToString()}";
            }
            else
            {
                InchesTextBlock.Text = " ";
            }
        }
    }
}
