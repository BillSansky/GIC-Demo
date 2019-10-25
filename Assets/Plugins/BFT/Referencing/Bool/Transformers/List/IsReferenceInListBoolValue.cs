using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BFT
{
    public class IsReferenceInListBoolValue<T> : SerializedMonoBehaviour, IValue<bool>
    {
        public IValue<T> Element;
        public IList<T> List;

        public bool Value => List.Contains(Element.Value);
    }
}
