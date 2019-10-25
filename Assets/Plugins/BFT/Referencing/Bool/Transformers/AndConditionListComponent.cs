using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class AndConditionListComponent : SerializedMonoBehaviour, IValue<bool>
    {
        public List<IValue<bool>> Conditions;

        [ShowInInspector, ReadOnly]
        public bool Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (Conditions == null)
                        return false;
                }
#endif

                foreach (var condition in Conditions)
                {
                    if (!condition.Value)
                        return false;
                }

                return true;
            }
        }
    }
}
