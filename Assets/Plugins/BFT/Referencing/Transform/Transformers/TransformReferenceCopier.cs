using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     copies the values of the referenced transform onto the targeted transform
    /// </summary>
    public class TransformReferenceCopier : MonoBehaviour
    {
        [BoxGroup("Options")] public bool AutoUpdate;

        [BoxGroup("Options")] public bool CopyPosition = true;
        [BoxGroup("Options")] public bool CopyRotation;
        [BoxGroup("Options")] public bool CopyScale;

        [BoxGroup("Transforms")] public UnityEngine.Transform Target;
        [BoxGroup("Transforms")] public TransformValue TransformReference;

        [BoxGroup("Options"), ShowIf("@ AutoUpdate && !UpdateOnLateUpdate")]
        public float UpdateFrequency = 0;

        [BoxGroup("Options")] public bool UpdateOnEnable = true;

        [BoxGroup("Options"), ShowIf("AutoUpdate")]
        public bool UpdateOnLateUpdate = false;

        void Reset()
        {
            if (!Target)
                Target = transform;
        }

        public void OnEnable()
        {
            if (UpdateOnEnable)
                CopyTransform();
        }

        public void CopyTransform()
        {
            UpdateTransform();
            if (AutoUpdate && !UpdateOnLateUpdate)
                StartCoroutine(AutoUpdateCoroutine());
        }

        public void ChangeTransformVariableToCopy(TransformVariableAsset toCopy)
        {
            TransformReference.Reference = toCopy;
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        [Button(ButtonSizes.Medium), BoxGroup("Tools")]
        private void UpdateTransform()
        {
            if (!TransformReference.Value)
                return;
            Target.Copy(TransformReference.Value, CopyPosition, CopyRotation, CopyScale);
        }

        private IEnumerator AutoUpdateCoroutine()
        {
            while (true)
            {
                UpdateTransform();
                yield return new WaitForSeconds(UpdateFrequency);
            }
        }

        private void LateUpdate()
        {
            if (!AutoUpdate || !UpdateOnLateUpdate)
                return;

            UpdateTransform();
        }
    }
}
