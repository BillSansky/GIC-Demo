using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class Vector3LerpVariableAsset : SerializedScriptableObject, IValue<Vector3>
    {
        public IValue<Vector3> EndValue;
        public IValue<Vector3> StartValue;

        public bool UsePerAxisComponents;

        public AnimationCurve XLerpCurve;
        public IValue<float> XPercent;

        [ShowIf("UsePerAxisComponents")] public AnimationCurve YLerpCurve;
        [ShowIf("UsePerAxisComponents")] public IValue<float> YPercent;

        [ShowIf("UsePerAxisComponents")] public AnimationCurve ZLerpCurve;
        [ShowIf("UsePerAxisComponents")] public IValue<float> ZPercent;

        public Vector3 Value
        {
            get
            {
                Vector3 outVector = new Vector3();
                outVector.x = Mathf.Lerp(StartValue.Value.x, EndValue.Value.x, XLerpCurve.Evaluate(XPercent.Value));
                outVector.y = Mathf.Lerp(StartValue.Value.y, EndValue.Value.y,
                    UsePerAxisComponents ? YLerpCurve.Evaluate(YPercent.Value) : XLerpCurve.Evaluate(XPercent.Value));
                outVector.z = Mathf.Lerp(StartValue.Value.z, EndValue.Value.z,
                    UsePerAxisComponents ? ZLerpCurve.Evaluate(ZPercent.Value) : XLerpCurve.Evaluate(XPercent.Value));

                return outVector;
            }
        }
    }
}
