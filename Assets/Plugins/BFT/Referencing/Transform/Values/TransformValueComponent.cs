using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformValueComponent : ValueComponent<UnityEngine.Transform, TransformValue>,
        IValue<UnityEngine.Transform>, IValue<Vector3>, IValue<UnityEngine.Quaternion>, IOrientation, IValue<IOrientation>
    {
        public TransformValue TransformValue;

        public Vector3 Up => Value.up;
        public Vector3 Right => Value.right;
        public Vector3 Forward => Value.forward;
        IOrientation IValue<IOrientation>.Value => this;
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => Value.rotation;
        UnityEngine.Transform IValue<UnityEngine.Transform>.Value => Value;

        Vector3 IValue<Vector3>.Value => Value.position;

        [Button(ButtonSizes.Medium)]
        public void Copy()
        {
            transform.Copy(Value);
        }
    }
}
