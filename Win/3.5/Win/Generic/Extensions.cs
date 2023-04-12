using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace MControl.Generic
{

    public static class StringExtension
    {
        public static string[] SplitTrim(this string s, params char[] spliter)
        {
            if (s == null || spliter == null)
            {
                throw new ArgumentNullException();
            }
            string[] array = s.Split(spliter);
            foreach (string a in array)
            {
                a.Trim();
            }
            return array;
        }
    }

    public static class EnumExtension
    {
        public static T Parse<T>(string value, T defaultValue)
        {
            try
            {
                if (value == null)
                    return defaultValue;
                if (!Enum.IsDefined(typeof(T), value))
                    return defaultValue;
                return (T) Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetDescription(Enum value)
        {
           //return Enumerations.GetEnumDescription(value);

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        //public static string GetDescription<TEnum>(int value)
        //{
        //    return GetDescription((Enum)(object)((TEnum)(object)value)); 
        //}

        //int value = 1;
        //string description = Enumerations.GetEnumDescription((MyEnum)value);

    }

    public static class GuidExtension
    {
        public static Guid NewUuid()
        {

            Guid guid;
            int result=  MControl.Win32.WinAPI.UuidCreateSequential(out guid);
            if (result == (int)MControl.Win32.WinAPI.RetUuidCodes.RPC_S_OK)
                return guid;
            else
                return Guid.NewGuid();
        }

        public static string GuidSegment()
        {
            return NewUuid().ToString().Split('-')[0];
        }
    }
  
}
