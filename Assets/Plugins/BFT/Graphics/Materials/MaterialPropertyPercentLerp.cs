using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class MaterialPropertyPercentLerp : MonoBehaviour
    {
        [BoxGroup("Lerping")] [ShowIf("PropertyType", MaterialPropertyType.COLOR)]
        public ColorValue MaxColorValue;

        [BoxGroup("Lerping")] [ShowIf("PropertyType", MaterialPropertyType.FLOAT)]
        public FloatValue MaxValue;

        [BoxGroup("Lerping")] [ShowIf("PropertyType", MaterialPropertyType.COLOR)]
        public ColorValue MinColorValue;

        [BoxGroup("Lerping")] [ShowIf("PropertyType", MaterialPropertyType.FLOAT)]
        public FloatValue MinValue;

        [BoxGroup("Lerping")] public PercentValue Percent;
        [BoxGroup("Lerping")] public AnimationCurve PercentProfile;

        private int propertyID;

        [BoxGroup("Property")] [ValueDropdown("PropertyIds")]
        public string PropertyName;

        [BoxGroup("Property")] [ValueDropdown("AuthorizedTypes")]
        public MaterialPropertyType PropertyType;

        public MeshRenderer Target;
        [BoxGroup("Options")] public bool UpdateEveryFrame;

        [BoxGroup("Options")] public bool UpdateOnEnable;

        [FormerlySerializedAs("Shared")] [BoxGroup("Options")]
        public bool UpdateSharedMaterial = true;

        private ValueDropdownList<MaterialPropertyType> AuthorizedTypes
        {
            get
            {
                ValueDropdownList<MaterialPropertyType> dd = new ValueDropdownList<MaterialPropertyType>();
                dd.Add("COLOR", MaterialPropertyType.COLOR);
                dd.Add("FLOAT", MaterialPropertyType.FLOAT);
                return dd;
            }
        }

#if UNITY_EDITOR
        private ValueDropdownList<string> PropertyIds =>
            ShaderUtilities.ShaderPropertyNames(Target.sharedMaterial, PropertyType);
#endif

        void Reset()
        {
            Target = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            propertyID = Shader.PropertyToID(PropertyName);

            if (UpdateOnEnable)
                UpdateMaterial();
            if (UpdateEveryFrame)
                StartCoroutine(UpdateMatEveryFrame());
        }

        private IEnumerator UpdateMatEveryFrame()
        {
            while (true)
            {
                UpdateMaterial();
                yield return null;
            }
        }

        [OnInspectorGUI]
        private void UpdateMaterial()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                propertyID = Shader.PropertyToID(PropertyName);
                UpdateMaterial(Target.sharedMaterial);
                return;
            }

#endif
            if (UpdateSharedMaterial)
                UpdateMaterial(Target.sharedMaterial);
            else
            {
                UpdateMaterial(Target.material);
            }
        }

        protected virtual void UpdateMaterial(Material mat)
        {
            switch (PropertyType)
            {
                case MaterialPropertyType.FLOAT:
                    Target.sharedMaterial.SetFloat(propertyID, GetValue());
                    break;
                case MaterialPropertyType.COLOR:
                    Target.sharedMaterial.SetColor(propertyID, GetColorValue());
                    break;
                case MaterialPropertyType.TEXTURE:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float GetValue()
        {
            return PercentUtil.CalculateFactor(MinValue.Value, MaxValue.Value, PercentProfile, Percent);
        }

        private Color GetColorValue()
        {
            return Color.Lerp(MinColorValue.Value, MaxColorValue.Value, PercentProfile.Evaluate(Percent.Value));
        }

        public void OnDrawGizmos()
        {
            if (Application.isPlaying)
                return;

            if (UpdateEveryFrame)
                UpdateMaterial();
        }
    }
}
