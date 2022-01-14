using System;
using System.Globalization;

namespace SqlStitcher.Forms.Infrastructure.ValueConverters
{
    public class EnumValueConverter<TEnum> : IValueConverter
        where TEnum : struct
    {
        public static readonly EnumValueConverter<TEnum> Default = new EnumValueConverter<TEnum>();

        public EnumValueConverter()
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException($"{nameof(TEnum)} must be an enum type.");
            }
        }

        public object Convert(object value, Type targetType, CultureInfo culture)
        {
            var enumValue = (TEnum?)value;

            if (!enumValue.HasValue)
            {
                throw new FormatException("Given value cannot be converted to an enum value.");
            }

            return (int)(object)enumValue.Value;
        }

        public object ConvertBack(object value, Type targetType, CultureInfo culture)
        {
            var intValue = (int?)value;

            if (!intValue.HasValue)
            {
                throw new FormatException("Given value cannot be converted to an integer.");
            }

            return ((TEnum)(object)intValue.Value);
        }
    }
}