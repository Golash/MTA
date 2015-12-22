using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI.Helpers
{
    internal static class ReflactionHelper
    {
        public static string GetConstValue(Type type, string constName)
        {
            string constValue = string.Empty;
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                if (fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && fieldInfo.Name == constName)
                {
                    constValue = (string)fieldInfo.GetValue(type);
                }
            }

            return constValue;
        }

        public static IEnumerable<Type> GetSubClasses<T>()
        {
            Type type = typeof(T);
            return Assembly.GetAssembly(type).GetTypes()
          .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(type));
        }
    }
}
