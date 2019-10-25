using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BFT
{
    public static class TypeUtils
    {
        public static IEnumerable<FieldInfo> GetAllFields(this Type t)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                                 BindingFlags.Static | BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
        }
    }
}
