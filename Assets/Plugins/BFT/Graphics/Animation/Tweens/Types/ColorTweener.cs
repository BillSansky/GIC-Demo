using UnityEngine;
#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public class ColorTweener : GeneralTweener<Color, ColorFunction, ColorAction, ColorValue>
    {
        protected override Tween BuildTween()
        {
            Tween = DOTween.To(DoGetter, DoSetter, EndValue.Value, TweenDuration.Value);
            return Tween;
        }
    }
}
#endif
