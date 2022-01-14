using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlStitcher.Forms.Helpers
{
    public static class ControlExtensions
    {
        public static T Clone<T>(this T controlToClone)
            where T : Control
        {
            var controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            T instance = Activator.CreateInstance<T>();

            foreach (var propInfo in controlProperties)
            {
                if (propInfo.CanWrite)
                {
                    if (!new[] { "WindowTarget", "LinkArea" }.Contains(propInfo.Name))
                        propInfo.SetValue(instance, propInfo.GetValue(controlToClone, null), null);
                }
            }

            return instance;
        }

        public static void BindEnum<TEnum>(this ComboBox comboBox)
        {
            var enumType = typeof(TEnum);

            var fields = enumType.GetMembers()
                                  .OfType<FieldInfo>()
                                  .Where(p => p.MemberType == MemberTypes.Field)
                                  .Where(p => p.IsLiteral)
                                  .ToList();


            var valuesByName = new Dictionary<string, object>();

            foreach (var field in fields)
            {
                var descriptionAttribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;

                var value = (int)field.GetValue(null);
                var description = string.Empty;

                if (!string.IsNullOrEmpty(descriptionAttribute?.Description))
                {
                    description = descriptionAttribute.Description;
                }
                else
                {
                    description = field.Name;
                }

                valuesByName[description] = value;
            }

            comboBox.DataSource = valuesByName.ToList();
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
        }


    }
}
