using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizzy.Pages.Tools.AllTools.Converns
{
    public class ConvertLeght
    {
        public int Unit { get; set; }
        public int Meters { get; set; }
        public int Millimeters { get; set; }
        public int Kilometers  { get; set; }
        public int Centimeters { get; set; }
        public int Decimeters { get; set; }
        public int Micrometers { get; set; }
        public int Nanometers { get; set; }
        public int Miles { get; set; }
        public int Yards { get; set; }
        public int Feet { get; set; }
        public int Inches { get; set; }





        public void ConvertMethod()
        {
            switch (Unit)
            {
                case 0:
                    Kilometers = Meters / 1000;
                    Centimeters = Meters * 100;
                    Millimeters = Meters * 1000;
                    Decimeters = Meters * 10;
                    Micrometers = Meters * 1000000;
                    Nanometers = Meters * 1000000000;
                    Miles = (int)(Meters * 1609.344);
                    Yards = (int)(Meters * 1.09361);
                    Feet = (int)(Meters * 3.28084);
                    Inches = (int)(Meters * 39.3701);
                    break;
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    
                    break;
                case 6:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid unit index");
            }
        }
    }


}
