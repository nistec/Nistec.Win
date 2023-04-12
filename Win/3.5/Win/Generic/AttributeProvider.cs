using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MControl.Generic
{

    public interface INaAttribute
    {
        bool IsNA { get; }
        bool IsIdentity { get; }
    }

    public class PropertuAttributeInfo<T>
    {
      public  PropertyInfo Property { get; set; }
      public  T Attribute { get; set; }
    }
    public class ParameteruAttributeInfo<T>
    {
        public ParameterInfo Parameter { get; set; }
        public T Attribute { get; set; }
        public int Position { get; set; }
    }

    #region CustomAttributeProvider Extensions

    public static class CustomAttributeProviderExtensions
    {
        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute
        {
            return GetCustomAttributes<T>(provider, true);
        }

        public static T[] GetCustomAttributes<T>(this ICustomAttributeProvider provider, bool inherit) where T : Attribute
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            T[] attributes = provider.GetCustomAttributes(typeof(T), inherit) as T[];
            if (attributes == null)
            {
                // WORKAROUND: Due to a bug in the code for retrieving attributes            
                // from a dynamic generated parameter, GetCustomAttributes can return            
                // an instance of an object[] instead of T[], and hence the cast above            
                // will return null.            
                return new T[0];
            }
            return attributes;
        }
    }
    #endregion

    public static class AttributeProvider
    {

        public static IEnumerable<PropertuAttributeInfo<T>> GetPropertiesInfo<T>(object instance)
        {

            IEnumerable<PropertuAttributeInfo<T>> props = from p in instance.GetType().GetProperties()
             let attr = p.GetCustomAttributes(typeof(T), true)
             where attr.Length == 1
            select new PropertuAttributeInfo<T>() { Property = p, Attribute = (T)attr.First() };

            return props;
        }

        public static IEnumerable<ParameteruAttributeInfo<T>> GetParametersInfo<T>(MethodInfo mi, int attributeLength )
        {

            IEnumerable<ParameteruAttributeInfo<T>> props = 
                from p in mi.GetParameters()
                let attr = p.GetCustomAttributes(typeof(T), true)
                where attr.Length == attributeLength
                orderby p.Position
                select new ParameteruAttributeInfo<T>() { Parameter = p, Attribute = (T)attr.FirstOrDefault(), Position = p.Position};

            return props;
        }

        public static T[] GetCustomAttributes<T>(object instance) where T : Attribute
        {
            return instance.GetType().GetCustomAttributes<T>();
        }

        public static T[] GetPropertiesAttributes<T>(object instance) where T : Attribute
        {
            return GetPropertiesAttributes<T>(instance, true, false);
        }

        public static T[] GetPropertiesAttributes<T>(object instance, bool canRead, bool canWrite) where T : Attribute
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            PropertyInfo[] properties = instance.GetType().GetProperties();
            List<T> list = new List<T>();

            foreach (PropertyInfo property in properties)
            {
                T attr = (T)Attribute.GetCustomAttribute(property, typeof(T));
                if (canWrite && !property.CanWrite)
                {
                    continue;
                }
                if (canRead && !property.CanRead)
                {
                    continue;
                }
                if (attr != null)
                {
                    if (attr is INaAttribute)
                    {
                        if (((INaAttribute)attr).IsNA)
                            continue;
                    }
                    list.Add(attr);
                }
            }

            return list.ToArray();
        }



        public static PropertyInfo[] GetActiveProperties<T>(object instance, bool canRead, bool canWright) where T : Attribute
        {

            PropertyInfo[] properties = instance.GetType().GetProperties();
            List<PropertyInfo> list = new List<PropertyInfo>();

            foreach (PropertyInfo property in properties)
            {

                T attr = (T)Attribute.GetCustomAttribute(property, typeof(T));

                if (attr != null)
                {
                    if (canRead && !property.CanRead)
                    {
                        continue;
                    }
                    if (canWright && !property.CanWrite)
                    {
                        continue;
                    }
                    if (attr is INaAttribute)
                    {
                        if (((INaAttribute)attr).IsNA)
                            continue;
                    }
                    list.Add(property);
                }
            }

            return list.ToArray();

        }

        public static PropertyInfo[] GetActiveProperties<T>(object instance, bool canRead, bool canWright, bool disableIdentity) where T : Attribute
        {

            PropertyInfo[] properties = instance.GetType().GetProperties();
            List<PropertyInfo> list = new List<PropertyInfo>();

            foreach (PropertyInfo property in properties)
            {

                T attr = (T)Attribute.GetCustomAttribute(property, typeof(T));

                if (attr != null)
                {
                    if (canRead && !property.CanRead)
                    {
                        continue;
                    }
                    if (canWright && !property.CanWrite)
                    {
                        continue;
                    }
                    if (attr is INaAttribute)
                    {
                        if (((INaAttribute)attr).IsNA)
                            continue;
                        if (disableIdentity && ((INaAttribute)attr).IsIdentity)
                            continue;
                    }
                    list.Add(property);
                }
            }

            return list.ToArray();

        }

    }
}
