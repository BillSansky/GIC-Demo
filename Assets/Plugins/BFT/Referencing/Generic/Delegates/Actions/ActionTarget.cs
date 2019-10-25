using System;
using System.Reflection;

namespace BFT
{
    [Serializable]
    public class ActionTarget
    {
        public string ActionName;
        public UnityEngine.Object TargetObject;

        public string Action
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
