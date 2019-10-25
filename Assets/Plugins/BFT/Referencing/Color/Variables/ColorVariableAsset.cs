using System;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    [Serializable]
    public class ColorEvent : UnityEvent<UnityEngine.Color>
    {
    }

    [CreateAssetMenu(menuName = "BFT/Data/Variables/Color Variable", fileName = "Color Variable")]
    public class ColorVariableAsset : VariableAsset<UnityEngine.Color>
    {
    }
}
