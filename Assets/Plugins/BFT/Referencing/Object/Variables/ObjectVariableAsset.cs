using System;
using UnityEngine.Events;

namespace BFT
{
    [Serializable]
    public class ObjectEvent : UnityEvent<object>
    {
    }

    public class ObjectVariableAsset : VariableAsset<object>
    {
    }
}
