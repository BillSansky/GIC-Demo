using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace BFT
{
    public class IntValueDropDownVariableAsset : VariableAsset<ValueDropdownList<int>>
    {
        [Button(ButtonSizes.Medium), BoxGroup("Tools")]
        public void AddNewEntry(string entry)
        {
            List<int> givenIDs = Value.Convert(_ => _.Value).ToList();

            Value.Add(entry, givenIDs.NextAvailableID());
        }
    }
}
