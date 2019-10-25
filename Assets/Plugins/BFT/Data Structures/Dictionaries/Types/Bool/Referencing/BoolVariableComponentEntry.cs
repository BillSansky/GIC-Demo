using System;

namespace BFT
{
    [Serializable]
    public class BoolVariableComponentEntry : ObjectEntry<BoolVariableComponent>, IDictionaryEntry<IVariable<bool>>
    {
        IVariable<bool> IDictionaryEntry<IVariable<bool>>.Data
        {
            get => Data;
            set => Data = (BoolVariableComponent) value;
        }


        public override JsonData ExportJsonData()
        {
            throw new System.NotImplementedException();
        }

        public override void ParseJsonData(JsonData data)
        {
            throw new System.NotImplementedException();
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
