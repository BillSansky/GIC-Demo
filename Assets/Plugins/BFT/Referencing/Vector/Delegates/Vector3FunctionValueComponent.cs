using UnityEngine;

namespace BFT
{
    public class Vector3FunctionValueComponent : FunctionValueComponent<Vector3>
    {
        public Vector3Function Vector3Function;
        public override Function<Vector3> Function => Vector3Function;
    }
}
