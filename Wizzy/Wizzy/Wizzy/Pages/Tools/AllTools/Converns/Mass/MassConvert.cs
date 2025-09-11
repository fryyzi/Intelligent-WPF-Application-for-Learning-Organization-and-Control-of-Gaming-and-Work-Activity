using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Wizzy.Pages.Tools.AllTools.Converns.Mass
{
    public class MassConvert
    {

        public int Unit { get; set; }
        public int Kilograms { get; set; }
        public int Grams { get; set; }
        public int Milligrams { get; set; }
        public int Tons { get; set; }



        public void MassConvertMethod()
        {
            switch (Unit)
            {
                case 0:
                    Grams = Kilograms * 1000;
                    Milligrams = Kilograms * 1000000;
                    Tons = Kilograms / 1000;
                    break;
                case 1:
                    Kilograms = Grams / 1000;
                    Milligrams = Grams * 1000;
                    Tons = Grams / 1000000;
                    break;
                case 2:
                    Kilograms = Milligrams / 1000000;
                    Grams = Milligrams / 1000;
                    Tons = Milligrams / 1000000000;
                    break;
                case 3:
                    Kilograms = Tons * 1000;
                    Grams = Tons * 1000000;
                    Milligrams = Tons * 1000000000;
                    break;
                default:
                    break;

            }
        }

    }
}
