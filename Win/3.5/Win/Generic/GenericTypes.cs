using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MControl
{
    public static class GenericTypes
    {

        public static T Default<T>()
        {
            return typeof(T).IsValueType ? Activator.CreateInstance<T>() : default(T);
        }

        /// <summary>
        /// Convert input to sepcified type, if input is null or unable to convert
        /// Creates an instance of the specified type using that type's default constructor.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertDefault(object input, Type type)
        {
            try
            {
                if (input == null || input == DBNull.Value)
                {
                    return type.IsValueType ? Activator.CreateInstance(type) : null;
                }

                var converter = TypeDescriptor.GetConverter(type);
                if (converter != null)
                {

                    return converter.ConvertFromString(input.ToString());
                }
            }
            catch
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
            return null;
        }

        public static T ConvertTo<T>(string input) where T : struct
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    T result = (T)converter.ConvertFromString(input);
                    return result;
                }
                catch
                {
                }
            }
            return Default<T>();
        }

        public static T ConvertTo<T>(string input, T defaultValue) where T : struct
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    T result = (T)converter.ConvertFromString(input);
                    return result;
                }
                catch
                {

                }
            }
            return defaultValue;
        }

        public static T ConvertObject<T>(object input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFrom(input);
                }
                return (T)input;
            }
            catch
            {
            }
            return Default<T>();
        }

        /// <summary>
        /// Generic Convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Convert<T>(object value)
        {
            //T defaultValue = Default<T>();
            //if (value == null || value == DBNull.Value)
            //    return Default<T>();
            //return ConvertFromString<T>(value.ToString());

            return Convert<T>(value, Default<T>());
        }

        /// <summary>
        /// Generic Convert with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public static T Convert<T>(object value, T defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;
            if (typeof(T) == typeof(Object))
                return (T)value;
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    return (T)converter.ConvertFromString(value.ToString());
                }
                catch { }
            }
            return defaultValue;
        }

        /// <summary>
        /// Generic implicit Convert an object with default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="valueIfNull"></param>
        /// <returns></returns>
        public static T ImplicitConvert<T>(object value, T defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;
            try
            {
                return (T)value;
            }
            catch { }
            return defaultValue;
        }

        /// <summary>
        /// Generic implicit Convert an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ImplicitConvert<T>(object value)
        {
            if (value == null || value == DBNull.Value)
                return Default<T>();
            try
            {
                return (T)value;
            }
            catch { }
            return Default<T>();
        }

        public static string NZorEmpty(object value, string valueIfNull)
        {
            try
            {
                return (value == null || value == DBNull.Value || value.ToString() == String.Empty) ? valueIfNull : value.ToString();
            }
            catch
            {
                return valueIfNull;
            }
        }

        public static T NZ<T>(object value, T valueIfNull)
        {
            try
            {
                return (value == null || value == DBNull.Value) ? valueIfNull : GenericTypes.Convert<T>(value, valueIfNull);
            }
            catch
            {
                return valueIfNull;
            }
        }

        public static T NZ<T>(object value)
        {
            try
            {
                return (value == null || value == DBNull.Value) ? Default<T>() : GenericTypes.Convert<T>(value);
            }
            catch
            {
                return Default<T>();
            }
        }

        /// <summary>
        /// TryParse 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static T? TryParse<T>(string value, TryParseHandler<T> handler) where T : struct
        {
            if (String.IsNullOrEmpty(value))
                return null;
            T result;
            if (handler(value, out result))
                return result;
            //Trace.TraceWarning("Invalid value '{0}'", value);
            return null;
        }
        /// <summary>
        /// TryParseHandler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public delegate bool TryParseHandler<T>(string value, out T result);


        /// <summary>  
        /// Checks the specified value to see if it can be  
        /// converted into the specified type.  
        /// <remarks>  
        /// The method supports all the primitive types of the CLR  
        /// such as int, boolean, double, guid etc. as well as other  
        /// simple types like Color and Unit and custom enum types.  
        /// </remarks>  
        /// </summary>  
        /// <param name="value">The value to check.</param>  
        /// <param name="type">The type that the value will be checked against.</param>  
        /// <returns>True if the value can convert to the given type, otherwise false. </returns>  
        public static bool CanConvert(string value, Type type)
        {
            if (string.IsNullOrEmpty(value) || type == null) return false;
            System.ComponentModel.TypeConverter conv = System.ComponentModel.TypeDescriptor.GetConverter(type);
            if (conv.CanConvertFrom(typeof(string)))
            {
                try
                {
                    conv.ConvertFrom(value);
                    return true;
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Is ? Can ConvertFrom
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool Is<T>(this string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            var conv = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (conv.CanConvertFrom(typeof(string)))
            {
                try
                {
                    conv.ConvertFrom(input);
                    return true;
                }
                catch { }
            } return false;
        }

        
        public static List<T> ConvertList<T>(object[] array)
        {
            return array.Cast<T>().ToList<T>();
        }
        public static List<T> ConvertList<T>(List<object> list)
        {
            return list.Cast<T>().ToList<T>();
        }
        public static T[] ConvertArray<T>(object[] array)
        {
            return Array.ConvertAll(array, item => (T)item);
        }
        //public static T ConvertItem<T>(object o)
        //{
        //    return new object[] { o }.Cast<T>().FirstOrDefault();
        //}

        public static T ConvertEntity<T>(object o)
        {
            if (o is T)
            {
                return (T)o;
            }
            else
            {
                try
                {
                    return (T) System.Convert.ChangeType(o, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }

        }
    }
}
