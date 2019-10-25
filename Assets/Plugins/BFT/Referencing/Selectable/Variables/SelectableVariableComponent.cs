using UnityEngine.EventSystems;

namespace BFT
{
    public class SelectableVariableComponent : VariableComponent<UnityEngine.UI.Selectable>
    {
        public SelectableVariable SelectableVariable;


        public override GenericVariable<UnityEngine.UI.Selectable> Variable => SelectableVariable;

        public void Select()
        {
            if (Value)
                Value.Select();
        }

        public void DeSelect()
        {
            if (Value && EventSystem.current.currentSelectedGameObject == Value.gameObject)
                EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
