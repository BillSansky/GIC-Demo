#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public class IntTweener : GeneralTweener<int, IntFunction, IntAction, IntValue>
    {
        protected override Tween BuildTween()
        {
            Tween = DOTween.To(DoGetter, DoSetter, EndValue.Value, TweenDuration.Value);
            return Tween;
        }
    }
}
#endif
