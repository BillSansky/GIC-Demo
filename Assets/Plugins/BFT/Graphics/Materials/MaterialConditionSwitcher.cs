using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class MaterialConditionSwitcher : SerializedMonoBehaviour
    {
        [BoxGroup("Condition")] public bool AutoCheck = false;

        [BoxGroup("Condition")] public IValue<bool> Condition;

        [BoxGroup("Condition")] [ShowIf("AutoCheck")]
        public bool IgnoreTimeScale;

        [BoxGroup("Render")] public Material MaterialToSwitchTo;

        [BoxGroup("Render"), ReadOnly] private Material originalMaterial;

        [BoxGroup("Render")] public Renderer Renderer;

        [BoxGroup("Condition")] public bool SwitchOnTrue = true;

        [BoxGroup("Condition")] [ShowIf("AutoCheck")]
        public float TimeBetweenChecks;

        void Reset()
        {
            Renderer = GetComponent<Renderer>();
        }

        public void Awake()
        {
            originalMaterial = Renderer.material;
        }

        public void OnEnable()
        {
            if (Condition == null || !Renderer)
                return;

            SwitchMaterialOnCondition();

            if (AutoCheck)
            {
                StopAllCoroutines();
                StartCoroutine(CheckMaterialSwitch());
            }
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CheckMaterialSwitch()
        {
            yield return CoroutineUtils.CallRegularly(TimeBetweenChecks,
                SwitchMaterialOnCondition, IgnoreTimeScale);
        }

        public void SwitchMaterialOnCondition()
        {
            if (SwitchOnTrue)
            {
                if (Condition.Value)
                    Renderer.material = MaterialToSwitchTo;
                else
                {
                    Renderer.material = originalMaterial;
                }
            }
            else if (!Condition.Value)
            {
                Renderer.material = MaterialToSwitchTo;
            }
            else
            {
                Renderer.material = originalMaterial;
            }
        }

        public void SwitchMaterial()
        {
            if (Renderer.material != MaterialToSwitchTo)
                Renderer.material = MaterialToSwitchTo;
            else
            {
                Renderer.material = originalMaterial;
            }
        }
    }
}
