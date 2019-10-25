using Sirenix.OdinInspector;

namespace BFT
{
    public class CameraVariableReferenceAsset : SerializedScriptableObject, IValue<UnityEngine.Camera>
    {
        public IValue<UnityEngine.Camera> ValueReference;
        public UnityEngine.Camera Value => ValueReference.Value;
    }
}
