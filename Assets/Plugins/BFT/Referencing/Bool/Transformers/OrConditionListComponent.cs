using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Bool/Or Bool Giver")]
    public class OrConditionListComponent : SerializedMonoBehaviour, IValue<bool>
    {
        public List<IValue<bool>> Conditions = new List<IValue<bool>>();

        [ShowInInspector, ReadOnly]
        public bool Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && Conditions == null)
                    return false;
#endif

                foreach (var condition in Conditions)
                {
                    if (condition.Value)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
