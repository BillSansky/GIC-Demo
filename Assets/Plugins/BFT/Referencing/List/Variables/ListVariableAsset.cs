using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace BFT
{
    [Serializable]
    public class ListEvent<T> : UnityEvent<IList<T>>
    {
    }

    public class ListVariableAsset<T, T2> :
        VariableAsset<IList<T>>
    {
    }
}
