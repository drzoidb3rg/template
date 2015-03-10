using System;
using System.Collections.Generic;
using System.Linq;
using Poe;
using Poe.Hypermedia;

namespace medtech.Extensions
{
    public class Option<T>
    {
        public T Key { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }

    public static class TypeExtenstions
    {
        public static List<Option<T>> EnumAsOptions<T>(this Type enumType, T selectedVal)
        {
            Array values = null;
            if (enumType.IsNullableEnum())
                values = Enum.GetValues(Nullable.GetUnderlyingType(enumType));
            else
                values = Enum.GetValues(enumType);
            var types = values.Cast<T>();
            var results = GetEnumKvp<T>(types, selectedVal).ToList();
            return results;
        }

        public static string GetEnumDescriptionOrDefault<T>(this Type type, T typeValue, string defaultValue = null)
        {
            var desc = type.GetEnumDescription(typeValue.ToString());
            if (!string.IsNullOrEmpty(desc))
                return desc;
            return defaultValue ?? typeValue.ToString().Underscore().Humanize();
        }

        static IEnumerable<Option<T>> GetEnumKvp<T>(IEnumerable<T> enumtypes, T selectedVal)
        {
            foreach (var type in enumtypes)
            {
                var desc = type.GetType().GetEnumDescription(type.ToString());
                if (string.IsNullOrEmpty(desc))
                    desc = type.ToString().Underscore().Humanize();
                yield return new Option<T>()
                {
                    Key = type,
                    Text = desc,
                    Selected = object.Equals(type, selectedVal)
                };
            }
        }

    }
}