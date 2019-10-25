using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor.Animations;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    public class AnimatorParameterSetOnStateEnter : SerializedStateMachineBehaviour
    {
        [ShowIf("IsBool")] public BoolValue BoolValue;

        [ShowIf("IsFloat")] public FloatValue FloatValue;

        [ShowIf("IsInt")] public IntValue IntValue;

        [ValueDropdown("ParameterIds")] public int ParameterID;

        public AnimatorControllerParameterType ParameterType;

#if UNITY_EDITOR
        public ValueDropdownList<int> ParameterIds
        {
            get
            {
                var dropDown = new ValueDropdownList<int>();

                if (Application.isPlaying)
                    return dropDown;

                var contexts = AnimatorController.FindStateMachineBehaviourContext(this);

                AnimatorController controller = null;
                foreach (var context in contexts)
                {
                    controller = context.animatorController;
                    if (controller)
                        break;
                }


                if (controller == null)
                    return dropDown;
                foreach (var param in controller.parameters.Where(_ =>
                    _.type == ParameterType))
                {
                    dropDown.Add(param.name, param.nameHash);
                }

                return dropDown;
            }
        }
#endif

        private bool IsBool => ParameterType == AnimatorControllerParameterType.Bool;
        private bool IsFloat => ParameterType == AnimatorControllerParameterType.Float;
        private bool IsInt => ParameterType == AnimatorControllerParameterType.Int;


        [Button(ButtonSizes.Medium)]
        public void SetAnimatorParameter(UnityEngine.Animator animator)
        {
            switch (ParameterType)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(ParameterID, FloatValue.Value);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(ParameterID, IntValue.Value);
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(ParameterID, BoolValue.Value);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    animator.SetTrigger(ParameterID);
                    break;
            }
        }

        public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            SetAnimatorParameter(animator);
        }
    }
}
