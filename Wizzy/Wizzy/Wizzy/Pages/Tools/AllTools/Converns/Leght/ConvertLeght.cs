namespace Wizzy.Pages.Tools.AllTools.Converns.Leght
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

                    Kilometers = Meters;

                    Meters = Kilometers * 1000.0;
                    Centimeters = Kilometers * 100000.0;
                    Millimeters = Kilometers * 1000000.0;
                    Decimeters = Kilometers * 10000.0;
                    Micrometers = Kilometers * 1000000000.0;
                    Nanometers = Kilometers * 1000000000000.0;
                    Miles = Kilometers * 0.621371;
                    Yards = Kilometers * 1093.61;
                    Feet = Kilometers * 3280.84;
                    Inches = Kilometers * 39370.1;
                    break;
                case 3:
                    Centimeters = Meters;

                    Meters = Centimeters / 100.0;
                    Kilometers = Centimeters / 100000.0;
                    Millimeters = Centimeters * 10.0;
                    Decimeters = Centimeters / 10.0;
                    Micrometers = Centimeters * 10000.0;
                    Nanometers = Centimeters * 10000000.0;
                    Miles = Centimeters * 0.0000062137;
                    Yards = Centimeters * 0.0109361;
                    Feet = Centimeters * 0.0328084;
                    Inches = Centimeters * 0.393701;
                    break;
                case 4:
                    Miles = Meters;

                    Meters = Miles * 1609.344;
                    Kilometers = Miles * 1.609344;
                    Centimeters = Miles * 160934.4;
                    Millimeters = Miles * 1609344.0;
                    Decimeters = Miles * 16093.44;
                    Micrometers = Miles * 1609344000.0;
                    Nanometers = Miles * 1609344000000.0;
                    Yards = Miles * 1760.0;
                    Feet = Miles * 5280.0;
                    Inches = Miles * 63360.0;
                    break;
                case 5:
                    Yards = Meters;

                    Meters = Yards / 1.09361;
                    Kilometers = Yards / 1093.61;
                    Centimeters = Yards * 91.44;
                    Millimeters = Yards * 914.4;
                    Decimeters = Yards * 9.144;
                    Micrometers = Yards * 914400.0;
                    Nanometers = Yards * 914400000.0;
                    Miles = Yards * 0.000568182;
                    Feet = Yards * 3.0;
                    Inches = Yards * 36.0;
                    break;
                case 6:
                    Feet = Meters;

                    Meters = Feet / 3.28084;
                    Kilometers = Feet / 3280.84;
                    Centimeters = Feet * 30.48;
                    Millimeters = Feet * 304.8;
                    Decimeters = Feet * 3.048;
                    Micrometers = Feet * 304800.0;
                    Nanometers = Feet * 304800000.0;
                    Miles = Feet * 0.000189394;
                    Yards = Feet / 3.0;
                    Inches = Feet * 12.0;
                    break;
                case 7:
                    Inches = Meters;

                    Meters = Inches / 39.3701;
                    Kilometers = Inches / 39370.1;
                    Centimeters = Inches * 2.54;
                    Millimeters = Inches * 25.4;
                    Decimeters = Inches * 0.254;
                    Micrometers = Inches * 25400.0;
                    Nanometers = Inches * 25400000.0;
                    Miles = Inches * 0.0000157828;
                    Yards = Inches / 36.0;
                    Feet = Inches / 12.0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Invalid unit index");
            }
        }
    }


}
