using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BFT
{
    public static class ShaderUtilities
    {
#if UNITY_EDITOR
        public static ValueDropdownList<string> ShaderPropertyNames(Material mat)
        {
            ValueDropdownList<string> dd = new ValueDropdownList<string>();
            dd.Add("None", "None");

            int count = ShaderUtil.GetPropertyCount(mat.shader);
            for (int i = 0; i < count; i++)
            {
                string name = ShaderUtil.GetPropertyName(mat.shader, i);
                dd.Add(name, name);
            }

            return dd;
        }

        public static ValueDropdownList<string> ShaderFloatPropertyNames(Material mat)
        {
            ValueDropdownList<string> dd = new ValueDropdownList<string>();
            dd.Add("None", "None");

            int count = ShaderUtil.GetPropertyCount(mat.shader);
            for (int i = 0; i < count; i++)
            {
                if (ShaderUtil.GetPropertyType(mat.shader, i) == ShaderUtil.ShaderPropertyType.Float ||
                    ShaderUtil.GetPropertyType(mat.shader, i) == ShaderUtil.ShaderPropertyType.Range)
                {
                    string name = ShaderUtil.GetPropertyName(mat.shader, i);
                    dd.Add(name, name);
                }
            }

            return dd;
        }

        public static ValueDropdownList<string> ShaderColorPropertyNames(Material mat)
        {
            ValueDropdownList<string> dd = new ValueDropdownList<string>();
            dd.Add("None", "None");
            int count = ShaderUtil.GetPropertyCount(mat.shader);
            for (int i = 0; i < count; i++)
            {
                if (ShaderUtil.GetPropertyType(mat.shader, i) == ShaderUtil.ShaderPropertyType.Color)
                {
                    string name = ShaderUtil.GetPropertyName(mat.shader, i);
                    dd.Add(name, name);
                }
            }

            return dd;
        }

        public static ValueDropdownList<string> ShaderTexturePropertyNames(Material mat)
        {
            ValueDropdownList<string> dd = new ValueDropdownList<string>();
            dd.Add("None", "None");

            int count = ShaderUtil.GetPropertyCount(mat.shader);
            for (int i = 0; i < count; i++)
            {
                if (ShaderUtil.GetPropertyType(mat.shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
                {
                    string name = ShaderUtil.GetPropertyName(mat.shader, i);
                    dd.Add(name, name);
                }
            }

            return dd;
        }


        public static ValueDropdownList<string> ShaderPropertyNames(Material mat, MaterialPropertyType type)
        {
            switch (type)
            {
                case MaterialPropertyType.FLOAT:
                    return ShaderFloatPropertyNames(mat);
                case MaterialPropertyType.COLOR:
                    return ShaderColorPropertyNames(mat);
                case MaterialPropertyType.TEXTURE:
                    return ShaderTexturePropertyNames(mat);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static int GetPropertyId(string name, Material mat)
        {
            int count = ShaderUtil.GetPropertyCount(mat.shader);
            for (int i = 0; i < count; i++)
            {
                if (ShaderUtil.GetPropertyName(mat.shader, i) == name)
                    return i;
            }

            return -1;
        }


#endif
    }
}
