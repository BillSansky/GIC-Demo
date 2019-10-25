namespace BFT
{
    public class ColliderPropertiesVariableComponent : VariableComponent<ColliderProperties>
    {
        public ColliderPropertiesVariable PropertiesVariable;
        public override GenericVariable<ColliderProperties> Variable => PropertiesVariable;
    }
}
