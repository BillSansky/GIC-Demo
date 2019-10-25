namespace BFT
{
    public class UnityObjectVariableAsset : VariableAsset<UnityEngine.Object>
    {
        public void SetValue(UnityEngine.Object newValue)
        {
            Value = newValue;
        }
    }
}
