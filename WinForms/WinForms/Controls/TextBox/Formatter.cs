using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace Nistec.WinForms
{
    internal class Formatter
    {
        // Fields
        private static Type booleanType = typeof(bool);
        private static Type checkStateType = typeof(CheckState);
        private static object defaultDataSourceNullValue = DBNull.Value;
        private static object parseMethodNotFound = new object();
        private static Type stringType = typeof(string);

        // Methods
        private static object ChangeType(object value, Type type, IFormatProvider formatInfo)
        {
            object obj2;
            try
            {
                if (formatInfo == null)
                {
                    formatInfo = CultureInfo.CurrentCulture;
                }
                obj2 = Convert.ChangeType(value, type, formatInfo);
            }
            catch (InvalidCastException exception)
            {
                throw new FormatException(exception.Message, exception);
            }
            return obj2;
        }

        private static bool EqualsFormattedNullValue(object value, object formattedNullValue, IFormatProvider formatInfo)
        {
            if ((formattedNullValue is string) && (value is string))
            {
                return (string.Compare((string)value, (string)formattedNullValue, true, GetFormatterCulture(formatInfo)) == 0);
            }
            return object.Equals(value, formattedNullValue);
        }

        public static object FormatObject(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
        {
            if (IsNullData(value, dataSourceNullValue))
            {
                value = DBNull.Value;
            }
            Type type = targetType;
            targetType = NullableUnwrap(targetType);
            sourceConverter = NullableUnwrap(sourceConverter);
            targetConverter = NullableUnwrap(targetConverter);
            bool flag = targetType != type;
            object obj2 = FormatObjectInternal(value, targetType, sourceConverter, targetConverter, formatString, formatInfo, formattedNullValue);
            if ((type.IsValueType && (obj2 == null)) && !flag)
            {
                throw new FormatException(GetCantConvertMessage(value, targetType));
            }
            return obj2;
        }

        private static object FormatObjectInternal(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue)
        {
            if ((value == DBNull.Value) || (value == null))
            {
                if (formattedNullValue != null)
                {
                    return formattedNullValue;
                }
                if (targetType == stringType)
                {
                    return string.Empty;
                }
                if (targetType == checkStateType)
                {
                    return CheckState.Indeterminate;
                }
                return null;
            }
            if (((targetType == stringType) && (value is IFormattable)) && !string.IsNullOrEmpty(formatString))
            {
                return (value as IFormattable).ToString(formatString, formatInfo);
            }
            Type type = value.GetType();
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (((sourceConverter != null) && (sourceConverter != converter)) && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, GetFormatterCulture(formatInfo), value, targetType);
            }
            TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
            if (((targetConverter != null) && (targetConverter != converter2)) && targetConverter.CanConvertFrom(type))
            {
                return targetConverter.ConvertFrom(null, GetFormatterCulture(formatInfo), value);
            }
            if (targetType == checkStateType)
            {
                if (type == booleanType)
                {
                    return (((bool)value) ? CheckState.Checked : CheckState.Unchecked);
                }
                if (sourceConverter == null)
                {
                    sourceConverter = converter;
                }
                if ((sourceConverter != null) && sourceConverter.CanConvertTo(booleanType))
                {
                    return (((bool)sourceConverter.ConvertTo(null, GetFormatterCulture(formatInfo), value, booleanType)) ? CheckState.Checked : CheckState.Unchecked);
                }
            }
            if (targetType.IsAssignableFrom(type))
            {
                return value;
            }
            if (sourceConverter == null)
            {
                sourceConverter = converter;
            }
            if (targetConverter == null)
            {
                targetConverter = converter2;
            }
            if ((sourceConverter != null) && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, GetFormatterCulture(formatInfo), value, targetType);
            }
            if ((targetConverter != null) && targetConverter.CanConvertFrom(type))
            {
                return targetConverter.ConvertFrom(null, GetFormatterCulture(formatInfo), value);
            }
            if (!(value is IConvertible))
            {
                throw new FormatException(GetCantConvertMessage(value, targetType));
            }
            return ChangeType(value, targetType, formatInfo);
        }

        private static string GetCantConvertMessage(object value, Type targetType)
        {
            string name = (value == null) ? "Formatter_CantConvertNull" : "Formatter_CantConvert";
            return string.Format(CultureInfo.CurrentCulture, name, new object[] { value, targetType.Name });
        }

        public static object GetDefaultDataSourceNullValue(Type type)
        {
            if ((type != null) && !type.IsValueType)
            {
                return null;
            }
            return defaultDataSourceNullValue;
        }

        private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
        {
            if (formatInfo is CultureInfo)
            {
                return (formatInfo as CultureInfo);
            }
            return CultureInfo.CurrentCulture;
        }

        public static object InvokeStringParseMethod(object value, Type targetType, IFormatProvider formatInfo)
        {
            object parseMethodNotFound;
            try
            {
                MethodInfo info = targetType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { stringType, typeof(NumberStyles), typeof(IFormatProvider) }, null);
                if (info != null)
                {
                    return info.Invoke(null, new object[] { (string)value, NumberStyles.Any, formatInfo });
                }
                info = targetType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { stringType, typeof(IFormatProvider) }, null);
                if (info != null)
                {
                    return info.Invoke(null, new object[] { (string)value, formatInfo });
                }
                info = targetType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { stringType }, null);
                if (info != null)
                {
                    return info.Invoke(null, new object[] { (string)value });
                }
                parseMethodNotFound = Formatter.parseMethodNotFound;
            }
            catch (TargetInvocationException exception)
            {
                throw new FormatException(exception.InnerException.Message, exception.InnerException);
            }
            return parseMethodNotFound;
        }

        public static bool IsNullData(object value, object dataSourceNullValue)
        {
            if ((value != null) && (value != DBNull.Value))
            {
                return object.Equals(value, NullData(value.GetType(), dataSourceNullValue));
            }
            return true;
        }

        private static TypeConverter NullableUnwrap(TypeConverter typeConverter)
        {
            NullableConverter converter = typeConverter as NullableConverter;
            if (converter == null)
            {
                return typeConverter;
            }
            return converter.UnderlyingTypeConverter;
        }

        private static Type NullableUnwrap(Type type)
        {
            if (type == stringType)
            {
                return stringType;
            }
            return (Nullable.GetUnderlyingType(type) ?? type);
        }

        public static object NullData(Type type, object dataSourceNullValue)
        {
            if (!type.IsGenericType || (type.GetGenericTypeDefinition() != typeof(Nullable<>)))
            {
                return dataSourceNullValue;
            }
            if ((dataSourceNullValue != null) && (dataSourceNullValue != DBNull.Value))
            {
                return dataSourceNullValue;
            }
            return null;
        }

        public static object ParseObject(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
        {
            Type type = targetType;
            sourceType = NullableUnwrap(sourceType);
            targetType = NullableUnwrap(targetType);
            sourceConverter = NullableUnwrap(sourceConverter);
            targetConverter = NullableUnwrap(targetConverter);
            object obj2 = ParseObjectInternal(value, targetType, sourceType, targetConverter, sourceConverter, formatInfo, formattedNullValue);
            if (obj2 == DBNull.Value)
            {
                return NullData(type, dataSourceNullValue);
            }
            return obj2;
        }

        private static object ParseObjectInternal(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue)
        {
            if (EqualsFormattedNullValue(value, formattedNullValue, formatInfo) || (value == DBNull.Value))
            {
                return DBNull.Value;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            if (((targetConverter != null) && (converter != targetConverter)) && targetConverter.CanConvertFrom(sourceType))
            {
                return targetConverter.ConvertFrom(null, GetFormatterCulture(formatInfo), value);
            }
            TypeConverter converter2 = TypeDescriptor.GetConverter(sourceType);
            if (((sourceConverter != null) && (converter2 != sourceConverter)) && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, GetFormatterCulture(formatInfo), value, targetType);
            }
            if (value is string)
            {
                object obj2 = InvokeStringParseMethod(value, targetType, formatInfo);
                if (obj2 != parseMethodNotFound)
                {
                    return obj2;
                }
            }
            else if (value is CheckState)
            {
                CheckState state = (CheckState)value;
                if (state == CheckState.Indeterminate)
                {
                    return DBNull.Value;
                }
                if (targetType == booleanType)
                {
                    return (state == CheckState.Checked);
                }
                if (targetConverter == null)
                {
                    targetConverter = converter;
                }
                if ((targetConverter != null) && targetConverter.CanConvertFrom(booleanType))
                {
                    return targetConverter.ConvertFrom(null, GetFormatterCulture(formatInfo), state == CheckState.Checked);
                }
            }
            else if ((value != null) && targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }
            if (targetConverter == null)
            {
                targetConverter = converter;
            }
            if (sourceConverter == null)
            {
                sourceConverter = converter2;
            }
            if ((targetConverter != null) && targetConverter.CanConvertFrom(sourceType))
            {
                return targetConverter.ConvertFrom(null, GetFormatterCulture(formatInfo), value);
            }
            if ((sourceConverter != null) && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, GetFormatterCulture(formatInfo), value, targetType);
            }
            if (!(value is IConvertible))
            {
                throw new FormatException(GetCantConvertMessage(value, targetType));
            }
            return ChangeType(value, targetType, formatInfo);
        }
    }
 

}
