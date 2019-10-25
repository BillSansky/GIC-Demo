using System;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class TransformVariable : GenericVariable<UnityEngine.Transform>, IValue<UnityEngine.Quaternion>, IValue<Vector3>
    {
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => Value.rotation;
        Vector3 IValue<Vector3>.Value => Value.position;
    }
}
