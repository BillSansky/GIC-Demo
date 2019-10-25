using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Data/Variables/Float Variable", fileName = "Float Variable")]
    public class FloatVariableAsset :
        VariableAsset<float>
    {
        public float FloatValue => Value;

        public void SetValueFromComponent(FloatValueComponent component)
        {
            Value = component.Value;
        }
    }
}
