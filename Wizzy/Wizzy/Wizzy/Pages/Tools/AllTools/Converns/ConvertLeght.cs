
namespace Wizzy.Pages.Tools.AllTools.Converns
{
    public class ConvertLeght
    {
        public double Unit { get; set; }
        public double Meters { get; set; }
        public double Millimeters { get; set; }
        public double Kilometers  { get; set; }
        public double Centimeters { get; set; }
        public double Decimeters { get; set; }
        public double Micrometers { get; set; }
        public double Nanometers { get; set; }
        public double Miles { get; set; }
        public double Yards { get; set; }
        public double Feet { get; set; }
        public double Inches { get; set; }





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
                    Millimeters = Meters;

                    Centimeters = Millimeters / 10.0;
                    Meters = Millimeters / 1000.0;
                    Kilometers = Millimeters / 1000000.0;
                    Micrometers = Millimeters * 1000.0;
                    Nanometers = Millimeters * 1000000.0;
                    Inches = Millimeters * 0.03937;
                    Feet = Millimeters * 0.003281;
                    Yards = Millimeters * 0.001094;
                    Miles = Millimeters * 0.000000621371;
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
