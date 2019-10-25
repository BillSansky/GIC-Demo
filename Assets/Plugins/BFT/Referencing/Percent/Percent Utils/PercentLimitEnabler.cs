using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Percent/On Percent Variable Enable")]
    public class PercentLimitEnabler : SerializedMonoBehaviour
    {
        public bool EnableWhenUnder = true;
        public IValue<float> Percent;
        public float PercentLimit;

        public UnityEngine.GameObject ToActivate;

        private void Update()
        {
            if (ToActivate.activeSelf)
            {
                if (EnableWhenUnder)
                {
                    if (Percent.Value >= PercentLimit)
                        ToActivate.SetActive(false);
                }
                else
                {
                    if (Percent.Value < PercentLimit)
                        ToActivate.SetActive(false);
                }
            }
            else
            {
                if (EnableWhenUnder)
                {
                    if (Percent.Value < PercentLimit)
                        ToActivate.SetActive(true);
                }
                else
                {
                    if (Percent.Value >= PercentLimit)
                        ToActivate.SetActive(true);
                }
            }
        }
    }
}
