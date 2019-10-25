#if BFT_REWIRED

using Rewired;
using Sirenix.OdinInspector;

namespace BFT
{
    public abstract class ButtonAction : SerializedMonoBehaviour
    {
        public void CheckButton(Player player, int buttonID)
        {
#if UNITY_EDITOR
            if (!ReInput.isReady)
            {
                return;
            }
#endif

            if (player.GetButtonDown(buttonID))
            {
                OnButtonDownAction();
            }
            else if (player.GetButtonUp(buttonID))
            {
                OnButtonUpAction();
            }

            if (player.GetButton(buttonID))
            {
                OnButtonHeldAction();
            }
        }

        public abstract void OnButtonDownAction();

        public abstract void OnButtonHeldAction();

        public abstract void OnButtonUpAction();
    }
}

#endif
