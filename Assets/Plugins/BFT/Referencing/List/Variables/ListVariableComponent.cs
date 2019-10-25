using System.Collections.Generic;

namespace BFT
{
    public class ListVariableComponent<T> : VariableComponent<IList<T>>
    {
        public override GenericVariable<IList<T>> Variable { get; }
    }
}
