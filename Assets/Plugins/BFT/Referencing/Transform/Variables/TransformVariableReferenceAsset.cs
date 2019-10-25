using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformVariableReferenceAsset : SerializedScriptableObject, IValue<UnityEngine.Transform>, IValue<Vector3>,
        IValue<UnityEngine.Quaternion>, IOrientation
    {
        public IValue<UnityEngine.Transform> ValueReference;

        public Vector3 Up => Value.up;
        public Vector3 Right => Value.right;
        public Vector3 Forward => Value.forward;
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => Value.rotation;
        public UnityEngine.Transform Value => ValueReference.Value;

        Vector3 IValue<Vector3>.Value => Value.position;
    }
}
