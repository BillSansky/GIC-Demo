using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformVariableComponent : VariableComponent<UnityEngine.Transform>,
        IValue<Vector3>, IValue<UnityEngine.Quaternion>, IOrientation, IValue<IOrientation>
    {
        public bool LocalSpaceForVectorValue;

        public TransformVariable TransformVariable;
        public override GenericVariable<UnityEngine.Transform> Variable => TransformVariable;

        public Vector3 Up => Value.up;
        public Vector3 Right => Value.right;
        public Vector3 Forward => Value.forward;
        IOrientation IValue<IOrientation>.Value => this;
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => Value.rotation;

        Vector3 IValue<Vector3>.Value => LocalSpaceForVectorValue ? Value.localPosition : Value.position;

        [Button(ButtonSizes.Medium)]
        public void Copy()
        {
            transform.Copy(Value);
        }

        public void SwitchValue(UnityEngine.Transform newReference)
        {
            Value = newReference;
        }
    }
}
