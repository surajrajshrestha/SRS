using EnumToListHelper.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace EnumToListHelper.Implementations
{
    public class EnumToListConverter : IEnumToList
    {
        public SelectList EnumSelectList<TEnum>(bool indexed = false)
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
            .Select(item => new SelectListItem
            {
                Text = GetEnumDescription(item as Enum),
                Value = indexed ? Convert.ToInt32(item).ToString() : item.ToString()
            }).OrderBy(x => x.Value).ToList(), "Value", "Text");
        }

        public string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public List<int> GetEnumToListValue<TEnum>(bool indexed = false)
            where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
                .Select(x => Convert.ToInt32(x)).ToList();
        }

        public T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }
    }
}
