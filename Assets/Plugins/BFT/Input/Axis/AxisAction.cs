using Sirenix.OdinInspector;
using UnityEngine.Events;
#if BFT_REWIRED
using Rewired;

namespace BFT
{
    public abstract class AxisAction : SerializedMonoBehaviour
    {
        private bool active = false;
        [BoxGroup("Activity")] public float activityThreshold = 0.01f;

        [BoxGroup("Activity")] public UnityEvent OnBecameActive;
        [BoxGroup("Activity")] public UnityEvent OnBecameInactive;

        public void CheckAxis(Player player, int axisID)
        {
            float value = player.GetAxis(axisID);

            if (value > activityThreshold)
            {
                if (!active)
                {
                    active = true;
                    OnBecameActive.Invoke();
                }
            }
            else
            {
                if (active)
                {
                    active = false;
                    OnBecameInactive.Invoke();
                }

                return;
            }

            AxisValueAction(value);
        }

        public abstract void AxisValueAction(float value);
    }
}
#endif
