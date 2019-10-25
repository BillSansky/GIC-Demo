using System;

namespace BFT
{
    [Serializable]
    public class TransformEntry : ObjectEntry<UnityEngine.Transform>
    {
        public override JsonData ExportJsonData()
        {
            throw new NotSupportedException();
        }

        public override void ParseJsonData(JsonData data)
        {
            throw new NotSupportedException();
        }

        public override void NotifyJSonDataDeleteRequest()
        {
            //nothing to do
        }
    }
}
