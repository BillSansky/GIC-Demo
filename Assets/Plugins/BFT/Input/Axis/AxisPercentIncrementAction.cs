using UnityEngine;
#if BFT_REWIRED
using Sirenix.OdinInspector;

namespace BFT
{
    public class AxisPercentIncrementAction : AxisAction
    {
        [BoxGroup("Constant Speed")] public bool AddOverThreshold = true;

        [BoxGroup("Constant Speed")] public float ConstantSpeedAddition;

        public float MaxDownSpeed;
        public float MaxUpSpeed;

        public IVariable<float> Percent;

        [BoxGroup("Constant Speed")] public float SpeedAdditionThreshold = 0;

        public override void AxisValueAction(float value)
        {
            float speed = value < 0 ? MaxDownSpeed : MaxUpSpeed;
            Percent.Value += speed * value * UnityEngine.Time.deltaTime;
            if ((AddOverThreshold && Percent.Value > SpeedAdditionThreshold)
                || (!AddOverThreshold && Percent.Value <= SpeedAdditionThreshold))
                Percent.Value += ConstantSpeedAddition * UnityEngine.Time.deltaTime;
            Percent.Value = Mathf.Clamp01(Percent.Value);
        }
    }
}

#endif
