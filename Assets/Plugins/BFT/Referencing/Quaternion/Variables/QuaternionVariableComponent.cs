namespace BFT
{
    public class QuaternionVariableComponent : VariableComponent<UnityEngine.Quaternion>
    {
        public QuaternionVariable QuaternionVariable;
        public override GenericVariable<UnityEngine.Quaternion> Variable => QuaternionVariable;
    }
}
