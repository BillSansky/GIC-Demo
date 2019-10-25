using UnityEngine;
#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public class AlphaTweener : GeneralTweener<Color, ColorFunction, ColorAction, ColorValue>
    {
        protected override Tween BuildTween()
        {
            Tween = DOTween.ToAlpha(DoGetter, DoSetter, EndValue.Value.a, TweenDuration.Value);
            return Tween;
        }
    }
}
#endif
