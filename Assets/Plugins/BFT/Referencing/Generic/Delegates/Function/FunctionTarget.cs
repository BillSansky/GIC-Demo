using System;
using System.Reflection;

namespace BFT
{
    [Serializable]
//[InlineProperty]
    public class FunctionTarget
    {
        public string ActionName;
        public UnityEngine.Object TargetObject;

        public string Function
        {
            get
            {
                if (!TargetObject || ActionName == null)
                    return "None";
                return TargetObject.GetType().Name + "." + ActionName;
            }
        }

        public MethodInfo MethodInfo => TargetObject.GetType().GetMethod(ActionName);
    }
}
