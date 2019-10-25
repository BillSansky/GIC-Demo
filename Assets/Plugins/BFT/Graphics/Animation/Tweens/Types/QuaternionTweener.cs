using DG.Tweening;
#if BFT_DOTWEEN
using UnityEngine;

namespace BFT
{
    public class QuaternionTweener : GeneralTweener<Quaternion, QuaternionFunction, QuaternionAction, QuaternionValue>
    {
        public Vector3 End => EndValue.Value.eulerAngles;

        protected override Tween BuildTween()
        {
            Tween = DOTween.To(DoGetter, DoSetter, End, TweenDuration.Value);
            return Tween;
        }
    }
}
#endif
