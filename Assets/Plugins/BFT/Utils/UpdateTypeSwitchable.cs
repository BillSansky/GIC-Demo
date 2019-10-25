using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public enum BehaviorUpdateType
    {
        UPDATE,
        FIXED_UPDATE,
        LATE_UPDATE,
    }

    [ExecuteInEditMode]
    public abstract class UpdateTypeSwitchable : MonoBehaviour
    {
        [FoldoutGroup("Update Type")] public BehaviorUpdateType BehaviorUpdateType = BehaviorUpdateType.UPDATE;

#if UNITY_EDITOR
        [FoldoutGroup("Utils")] public bool ExecuteInEditMode = true;
#endif

        [FoldoutGroup("Utils", false, 999)]
        [Button(ButtonSizes.Medium)]
        public abstract void UpdateMethod();

        void FixedUpdate()
        {
            if (BehaviorUpdateType != BehaviorUpdateType.FIXED_UPDATE)
                return;

            UpdateMethod();
        }

        void Update()
        {
#if UNITY_EDITOR

            if (!Application.isPlaying)
            {
                if (!ExecuteInEditMode)
                    return;

                UpdateMethod();
                return;
            }

#endif
            if (BehaviorUpdateType == BehaviorUpdateType.UPDATE)
                UpdateMethod();
        }

        void LateUpdate()
        {
#if UNITY_EDITOR

            if (!Application.isPlaying)
                return;
#endif

            if (BehaviorUpdateType != BehaviorUpdateType.LATE_UPDATE)
                return;

            UpdateMethod();
        }
    }
}
