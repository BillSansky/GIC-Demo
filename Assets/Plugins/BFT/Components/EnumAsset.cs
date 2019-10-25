using UnityEngine;

namespace BFT
{
    public class EnumAsset : ScriptableObject
    {
        /// <summary>
        ///     returns true if the two enums are equal, or if the first one is null
        /// </summary>
        /// <param name="firstEnum"></param>
        /// <param name="secondEnum"></param>
        /// <returns></returns>
        public static bool IsEnumCompatible(EnumAsset firstEnum, EnumAsset secondEnum)
        {
            if (firstEnum == null)
                return true;
            if (firstEnum == secondEnum)
                return true;

            return false;
        }
    }
}
