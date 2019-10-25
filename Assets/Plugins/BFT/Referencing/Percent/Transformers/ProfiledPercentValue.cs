using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [AddComponentMenu("Referencing/Percent/Profiled Percent")]
    public class ProfiledPercentValue : SerializedMonoBehaviour, IValue<float>
    {
        public IValue<float> PercentGiverToProfile;
        public AnimationCurve Profile;

        [ShowInInspector, ReadOnly]
        public float Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && Profile == null || PercentGiverToProfile == null)
                    return 0.5f;
#endif
                return Mathf.Clamp01(Profile.Evaluate(PercentGiverToProfile.Value));
            }
        }
    }
}
