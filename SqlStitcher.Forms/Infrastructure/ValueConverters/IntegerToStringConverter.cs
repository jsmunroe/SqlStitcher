using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlStitcher.Forms.Infrastructure.ValueConverters
{
    public class IntegerToStringConverter : IValueConverter
    {
        public static readonly IntegerToStringConverter Default = new IntegerToStringConverter();

        public object Convert(object value, Type targetType, CultureInfo culture)
        {
            if (value is Int32 == false)
            {
                throw new FormatException("Given value cannot be converted to an integer.");
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, CultureInfo culture)
        {
            if (value is string == false)
            {
                throw new FormatException("Given value cannot be converted to an integer.");
            }

            return Int32.Parse((string)value);
        }
    }
}
