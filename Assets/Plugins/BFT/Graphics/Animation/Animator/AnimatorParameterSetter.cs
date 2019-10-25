using System;
using System.Collections;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class AnimatorParameterSetter : MonoBehaviour
    {
        public UnityEngine.Animator animator;

        [ShowIf("IsBool")] public BoolValue BoolValue;


        [ShowIf("IsFloat")] public FloatValue FloatValue;

        [ShowIf("IsInt")] public IntValue IntValue;
        private bool isUnsafe = false;

        public bool LogDebug;

        [ValueDropdown("ParameterIds")] public int ParameterID;

        public AnimatorControllerParameterType ParameterType;

        public bool PreventFewTriggersPerFrame = true;
        public bool SetOnEnable;


        [ShowIf("SetOnEnable")] public bool UpdateEveryFrame = false;

#if UNITY_EDITOR
        public ValueDropdownList<int> ParameterIds
        {
            get
            {
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                    return new ValueDropdownList<int>()
                    {
                        {"Current Value", ParameterID}
                    };

                var dropDown = new ValueDropdownList<int>();

                if (!animator || Application.isPlaying)
                    return dropDown;

                var controller =
                    UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.Animations.AnimatorController>(
                        UnityEditor.AssetDatabase.GetAssetPath(animator.runtimeAnimatorController));
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

        public void Reset()
        {
            animator = GetComponent<UnityEngine.Animator>();
        }

        public void OnEnable()
        {
            if (SetOnEnable)
            {
                SetAnimatorParameter();
                if (UpdateEveryFrame)
                    StartSetOnEveryFrame();
            }
        }

        public void OnDisable()
        {
            isUnsafe = false;
        }

        private void StartSetOnEveryFrame()
        {
            StartCoroutine(SetParameterEveryFrame());
        }

        [Button(ButtonSizes.Medium)]
        public void SetAnimatorParameter()
        {
            if (!animator.enabled || !animator.gameObject.activeInHierarchy)
            {
                if (LogDebug)
                {
                    UnityEngine.Debug.Assert(animator.enabled && animator.gameObject.activeInHierarchy,
                        "Setting the animator parameter while the animator is disabled may not register the change", this);
                }

                return;
            }


            if (isUnsafe)
                return;

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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (PreventFewTriggersPerFrame)
                StartCoroutine(MakeUnsafeTillEndOfFrame());

            if (LogDebug)
                UnityEngine.Debug.Log("Parameter Set", this);
        }

        private IEnumerator MakeUnsafeTillEndOfFrame()
        {
            isUnsafe = true;
            yield return null;
            isUnsafe = false;
        }

        private IEnumerator SetParameterEveryFrame()
        {
            while (true)
            {
                SetAnimatorParameter();
                yield return null;
            }
        }
    }
}
