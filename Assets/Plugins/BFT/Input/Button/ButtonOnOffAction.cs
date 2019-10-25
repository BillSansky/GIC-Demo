using UnityEngine;
#if BFT_REWIRED
using Sirenix.OdinInspector;

namespace BFT
{
    public class ButtonOnOffAction : ButtonAction
    {
        [BoxGroup("Reference")] public IVariable<bool> ButtonStateReference;

        [BoxGroup("Debug")] public bool LogButton = true;

        [BoxGroup("Options")] public bool TrueOnButtonDown = true;

        public override void OnButtonDownAction()
        {
            ButtonStateReference.Value = TrueOnButtonDown;

            if (LogButton)
                UnityEngine.Debug.Log("Button Down : " + gameObject.name);
        }

        public override void OnButtonHeldAction()
        {
        }

        public override void OnButtonUpAction()
        {
            ButtonStateReference.Value = !TrueOnButtonDown;

            if (LogButton)
                UnityEngine.Debug.Log("Button Up : " + gameObject.name);
        }
    }
}

#endif
