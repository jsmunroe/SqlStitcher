using System;
using System.Globalization;
using System.Windows.Forms;

namespace SqlStitcher.Forms.Infrastructure.ValueConverters
{
    public class CheckStateConverter : IValueConverter
    {
        public static readonly CheckStateConverter Default = new CheckStateConverter();

        public object Convert(object value, Type targetType, CultureInfo culture)
        {
            var boolValue = (bool?)value;

            if (!boolValue.HasValue)
            {
                throw new FormatException("Given value cannot be converted to an Boolean value.");
            }

            return (boolValue.Value) ? CheckState.Checked : CheckState.Unchecked;
        }

        public object ConvertBack(object value, Type targetType, CultureInfo culture)
        {
            var checkStateValue = (CheckState?)value;

            if (!checkStateValue.HasValue)
            {
                throw new FormatException("Given value cannot be converted to an CheckState.");
            }

            return checkStateValue == CheckState.Checked;
        }
    }
}