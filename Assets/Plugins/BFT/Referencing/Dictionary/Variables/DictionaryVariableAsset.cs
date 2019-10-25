using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace BFT
{
    [Serializable]
    public class DictionaryEvent<T1, T2> : UnityEvent<Dictionary<T1, T2>>
    {
    }

    public class DictionaryVariableAsset<T1, T2> : VariableAsset<Dictionary<T1, T2>>
    {
    }
}
