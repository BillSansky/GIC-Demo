using System.Collections.Generic;
using System.Text;

namespace BFT
{
    public static class JSonDataUtils
    {
        public static T GetAssetReferenceFromName<T>(string name) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            string[] assetGUIDForSprite = UnityEditor.AssetDatabase.FindAssets(name + "t:" + typeof(T).Name);
            T field = default(T);
            if (assetGUIDForSprite.Length > 0)
            {
                field = (T) UnityEditor.AssetDatabase.LoadAssetAtPath(
                    UnityEditor.AssetDatabase.GUIDToAssetPath(assetGUIDForSprite[0]), typeof(T));
            }

            return field;
#else
        throw new PlatformNotSupportedException();
#endif
        }

        public static void AddArrayOfReferenceObjects(string variableName, JsonData data, IEnumerable<UnityEngine.Object> objects)
        {
            StringBuilder builder = new StringBuilder();

            bool start = true;
            foreach (UnityEngine.Object obj in objects)
            {
                if (!start)
                {
                    builder.Append(",");
                }

                builder.Append(obj.name);
                start = false;
            }

            data.DataByID.Add(variableName, builder.ToString());
        }
    }
}
