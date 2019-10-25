using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorStateMachineComponent : MonoBehaviour
    {
        [HideIf("UseInNestedPrefab")] public Dictionary<int, AnimatorStateBehaviorComponent> BehaviorComponentPerState
            = new Dictionary<int, AnimatorStateBehaviorComponent>();


        [ShowIf("UseInNestedPrefab")] public List<AnimatorStateBehaviorComponent> Components;

        public void Awake()
        {
            BehaviorComponentPerState = new Dictionary<int, AnimatorStateBehaviorComponent>(Components.Count);

            foreach (var component in Components)
            {
                if (!BehaviorComponentPerState.ContainsKey(component.StateID))
                {
                    BehaviorComponentPerState.Add(component.StateID, component);
                }
            }
        }
    }
}
