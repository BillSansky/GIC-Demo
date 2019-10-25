using UnityEngine;
#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public class Vector3Tweener : GeneralTweener<Vector3, Vector3Function, Vector3Action, Vector3Value>
    {
        protected override Tween BuildTween()
        {
            Tween = DOTween.To(DoGetter, DoSetter, EndValue.Value, TweenDuration.Value);
            return Tween;
        }
    }
}

#endif
