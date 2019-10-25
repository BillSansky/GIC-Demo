using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Event/Int Event Invoker", fileName = "IntEventInvoker")]
    public class IntEventAsset : GenericEventAsset<int, IntEvent>
    {
    }
}
