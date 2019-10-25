using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BFT
{
    public static class ValueDropDownExt
    {
        public static T GetValueFromString<T>(this ValueDropdownList<T> list, string name)
        {
            foreach (var item in list)
            {
                if (item.Text == name)
                {
                    return item.Value;
                }
            }

            return default(T);
        }

        public static ValueDropdownList<int> DropDownFromIDDictionary<T>(Dictionary<int, T> dictionary,
            Func<T, string> valueToString)
        {
            ValueDropdownList<int> dropDown = new ValueDropdownList<int>();
            foreach (var pair in dictionary)
            {
                dropDown.Add(valueToString(pair.Value), pair.Key);
            }

            return dropDown;
        }
    }
}
