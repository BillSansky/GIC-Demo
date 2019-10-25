using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Event/String Event Invoker", fileName = "StringEventInvoker")]
    public class StringEventAsset : GenericEventAsset<string, StringEvent>
    {
    }
}
