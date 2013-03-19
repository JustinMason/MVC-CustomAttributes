using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MVC.Validation.DateComparer
{
    public class ReflectionHelper
    {
        public static object GetPropValue(object src, string propName)
        {
            return GetProperty(src, propName).GetValue(src, null);
        }

        public static PropertyInfo GetProperty(object src, string propName)
        {
            var result = src.GetType().GetProperty(propName);
            if (result == null)
            {
                throw new ApplicationException(string.Format("Type: {0} does not contain the Property: {1} ", src.GetType().FullName, propName));
            }

            return result;
        }
    }
}
