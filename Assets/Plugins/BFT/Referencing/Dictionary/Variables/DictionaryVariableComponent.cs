using System.Collections.Generic;

namespace BFT
{
    public class DictionaryVariableComponent<T1, T2> : VariableComponent<Dictionary<T1, T2>>
    {
        public override GenericVariable<Dictionary<T1, T2>> Variable { get; }
    }
}
