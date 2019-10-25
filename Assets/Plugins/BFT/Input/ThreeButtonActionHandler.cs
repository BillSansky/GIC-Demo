#if BFT_REWIRED
using UnityEngine;
using Rewired;

namespace BFT
{
    public class ThreeButtonActionHandler : MonoBehaviour
    {
        public int ButtonOneInputId;

        public ButtonAction ButtonOnePinnedAction;
        public int ButtonThreeInputId;
        public ButtonAction ButtonThreePinnedAction;
        public int ButtonTwoInputId;
        public ButtonAction ButtonTwoPinnedAction;

        public void CheckInputsForPlayer(Player player)
        {
            ButtonOnePinnedAction.CheckButton(player, ButtonOneInputId);
            ButtonTwoPinnedAction.CheckButton(player, ButtonTwoInputId);
            ButtonThreePinnedAction.CheckButton(player, ButtonThreeInputId);
        }
    }
}

#endif
