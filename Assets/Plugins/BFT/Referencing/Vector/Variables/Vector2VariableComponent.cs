using UnityEngine;

namespace BFT
{
    public class Vector2VariableComponent : VariableComponent<Vector2>
    {
        public Vector2Variable Vector2Variable;
        public override GenericVariable<Vector2> Variable => Vector2Variable;
    }
}
