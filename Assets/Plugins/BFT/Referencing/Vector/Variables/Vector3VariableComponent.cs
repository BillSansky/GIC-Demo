using UnityEngine;

namespace BFT
{
    public class Vector3VariableComponent : VariableComponent<Vector3>
    {
        public Vector3Variable Vector3Variable;
        public override GenericVariable<Vector3> Variable => Vector3Variable;
    }
}
