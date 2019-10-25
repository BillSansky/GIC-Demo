#if BFT_DOTWEEN
using DG.Tweening;

namespace BFT
{
    public interface ITweenValue : IAnimation, IValue<Tween>
    {
    }
}
#endif
