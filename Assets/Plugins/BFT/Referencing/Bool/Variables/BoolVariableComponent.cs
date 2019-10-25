using Sirenix.OdinInspector;

namespace BFT
{
    public class BoolVariableComponent : VariableComponent<bool>
    {
        public BoolVariable BoolVariable;

        [BoxGroup("Events"), ShowIf("emitUnityEvent")]
        public BoolEvent OnValueFalse;

        [BoxGroup("Events"), ShowIf("emitUnityEvent")]
        public BoolEvent OnValueTrue;

        public override GenericVariable<bool> Variable => BoolVariable;

        public object Save()
        {
            return Value;
        }

        public void Load(object saveFile)
        {
            Value = (bool) saveFile;
        }

        [FoldoutGroup("Tool", Order = 9999999), Button(ButtonSizes.Medium)]
        public void SwitchValue()
        {
            Value = !Value;
        }

        protected override void EmitEvent()
        {
            base.EmitEvent();
            if (Value)
                OnValueTrue.Invoke(true);
            else
            {
                OnValueFalse.Invoke(false);
            }
        }

        public void SetValueAsOr(BoolVariableComponent first, BoolVariableComponent second)
        {
            Value = first.Value || second.Value;
        }
    }
}
