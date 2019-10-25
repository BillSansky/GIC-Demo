using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace BFT
{
    [CreateAssetMenu(menuName = "Data/Variables/Int Variable", fileName = "Int Variable")]
    public class IntVariableAsset :
        VariableAsset<int>, IValue<float>, IValue<string>
    {
        public object Save()
        {
            return Value;
        }

        public void Load(object saveFile)
        {
            Value = (int) saveFile;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        float IValue<float>.Value => Value;

        string IValue<string>.Value => Value.ToString();

        public void Increment()
        {
            Value++;
        }

        public void Decrement()
        {
            Value--;
        }

        public void Add(int amount)
        {
            Value += amount;
        }
    }
}
