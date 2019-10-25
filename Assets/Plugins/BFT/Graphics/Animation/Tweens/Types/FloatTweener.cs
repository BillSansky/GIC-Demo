#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public class FloatTweener : GeneralTweener<float, FloatFunction, FloatAction, FloatValue>
    {
        protected override Tween BuildTween()
        {
            Tween = DOTween.To(DoGetter, DoSetter, EndValue.Value, TweenDuration.Value);
            return Tween;
        }
    }
}

#endif
