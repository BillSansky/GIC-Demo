using System;
using UnityEngine.Events;

namespace BFT
{
    public class SelectableVariableAsset :
        VariableAsset<UnityEngine.UI.Selectable>
    {
    }

    [Serializable]
    public class SelectableEvent : UnityEvent<UnityEngine.UI.Selectable>
    {
    }
}
