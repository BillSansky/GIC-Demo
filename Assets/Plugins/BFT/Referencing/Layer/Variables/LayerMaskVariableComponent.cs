using UnityEngine;

namespace BFT
{
    public class LayerMaskVariableComponent : VariableComponent<LayerMask>
    {
        public LayerMaskVariable LayerMaskVariable;
        public override GenericVariable<LayerMask> Variable => LayerMaskVariable;
    }
}
