using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class AnimatorStateBehaviorComponent : MonoBehaviour
    {
        public Animator Animator;

        [ValueDropdown("States")] public int StateID;

#if UNITY_EDITOR
        private ValueDropdownList<int> States
        {
            get
            {
                ValueDropdownList<int> drop = new ValueDropdownList<int>();

                var controller =
                    UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.Animations.AnimatorController>(
                        UnityEditor.AssetDatabase.GetAssetPath(Animator.runtimeAnimatorController));
                if (controller == null)
                    return drop;

                void AddMachineStates(UnityEditor.Animations.AnimatorStateMachine animatorStateMachine)
                {
                    foreach (var state in animatorStateMachine.states)
                    {
                        foreach (var link in state.state.behaviours.Where(_ => _ is StateComponentLink))
                        {
                            drop.Add(state.state.name, ((StateComponentLink) link).id);
                        }
                    }

                    foreach (var machine in animatorStateMachine.stateMachines)
                    {
                        AddMachineStates(machine.stateMachine);
                    }
                }

                foreach (var layer in controller.layers)
                {
                    var machine = layer.stateMachine;
                    AddMachineStates(machine);
                }

                return drop;
            }
        }
#endif

        public virtual void OnStateEntered()
        {
        }

        public virtual void OnStateExit()
        {
        }

        private void Reset()
        {
            Animator = transform.parent.gameObject.GetComponent<Animator>();
        }
    }
}
