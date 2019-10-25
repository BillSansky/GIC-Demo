using UnityEngine;

namespace BFT
{
    public class AdvancedTransform : MonoBehaviour, IValue<UnityEngine.Transform>, IValue<Vector3>, IValue<UnityEngine.Quaternion>
    {
        UnityEngine.Quaternion IValue<UnityEngine.Quaternion>.Value => transform.rotation;
        UnityEngine.Transform IValue<UnityEngine.Transform>.Value => transform;

        Vector3 IValue<Vector3>.Value => transform.position;
    }
}
