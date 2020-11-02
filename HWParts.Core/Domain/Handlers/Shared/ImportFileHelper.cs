using System;
using System.Globalization;

namespace HWParts.Core.Domain.Handlers.Shared
{
    public class ImportFileHelper
    {
        public static T BindParameter<T>(string parameter, dynamic item)
        {
            return item[parameter] is null ? typeof(T) == typeof(string) ? "" : Activator.CreateInstance(typeof(T)) : item[parameter].ToObject<T>();
        }

        public static DateTime? BindDateParameter(string parameter, dynamic item, bool culture)
        {
            if (culture)
            {
                return item[parameter] is null ? null : DateTime.ParseExact(item[parameter].ToObject<string>(), "dd/MM/yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("pt-BR"));
            }

            return item[parameter]?.ToObject<DateTime>();
        }
    }
}
