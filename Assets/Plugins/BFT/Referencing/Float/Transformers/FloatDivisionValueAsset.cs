using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Float Division Variable", fileName = "Float Division Variable")]
    public class FloatDivisionValueAsset : SerializedScriptableObject, IValue<float>
    {
        public IValue<float> DividerValue;
        public IValue<float> FloatValue;

        [BoxGroup("Status"), ReadOnly, ShowIf("@FloatValue!=null && DividerValue!=null"), ShowInInspector]
        public float Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && (FloatValue == null || DividerValue == null))
                {
                    return -1;
                }
#endif

                if (Mathf.Abs(DividerValue.Value) <= 0)
                {
                    UnityEngine.Debug.LogWarning("Attempting to divide by zero!", this);
                    return 0;
                }

                return FloatValue.Value / DividerValue.Value;
            }
        }
    }
}
