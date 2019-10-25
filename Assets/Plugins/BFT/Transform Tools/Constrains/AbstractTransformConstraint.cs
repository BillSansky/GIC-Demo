using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public abstract class AbstractTransformConstraint : MonoBehaviour
    {
        [FormerlySerializedAs("Order")] public int ConstrainOrder;
        public abstract void Constrain();

        public virtual void OnEnable()
        {
            TransformConstraintsHandler.Instance.AddConstraint(this);
        }

        public virtual void OnDisable()
        {
            TransformConstraintsHandler.Instance.RemoveConstraint(this);
        }
    }
}
