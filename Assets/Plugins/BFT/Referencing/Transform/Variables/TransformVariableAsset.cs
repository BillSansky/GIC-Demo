using UnityEngine;

namespace BFT
{
    public class TransformVariableAsset : VariableAsset<UnityEngine.Transform>, IValue<UnityEngine.Quaternion>,
        IValue<Vector3>, IOrientation
    {
        public Vector3 Up => Value ? Value.up : Vector3.up;
        public Vector3 Right => Value ? Value.right : Vector3.right;
        public Vector3 Forward => Value ? Value.forward : Vector3.forward;
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => Value.rotation;
        Vector3 IValue<Vector3>.Value => Value.position;
    }
}
