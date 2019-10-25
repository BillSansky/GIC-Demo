using UnityEngine;

namespace BFT
{
    public class RandomVectorVariableComponent : MonoBehaviour, IValue<Vector3>
    {
        public float MaxDistanceFromCenter;
        public float MinDistanceFromCenter;
        public Vector3 PositionFilter;
        public Vector3 ReferenceCenter;

        public Vector3 Value
        {
            get
            {
                float radius = Random.Range(MinDistanceFromCenter, MaxDistanceFromCenter);
                return RandomExt.RandomSphereSurfacePoint(ReferenceCenter, radius).Mult(PositionFilter);
            }
        }
    }
}
