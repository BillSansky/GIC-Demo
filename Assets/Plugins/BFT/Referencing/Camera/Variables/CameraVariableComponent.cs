namespace BFT
{
    public class CameraVariableComponent : VariableComponent<UnityEngine.Camera>
    {
        public CameraVariable Camera;
        public override GenericVariable<UnityEngine.Camera> Variable => Camera;
    }
}
