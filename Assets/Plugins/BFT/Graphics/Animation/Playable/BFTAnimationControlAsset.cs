using UnityEngine;
using UnityEngine.Playables;

namespace BFT
{
    public class BFTAnimationControlAsset : PlayableAsset
    {
        public bool HoldOnDone;
        public bool PlayReversed;
        public IAnimation Target;

        public override UnityEngine.Playables.Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<BFTAnimationPlayableBehaviour>.Create(graph);
            var behavior = playable.GetBehaviour();
            behavior.Target = Target;
            behavior.HoldOnDone = HoldOnDone;
            behavior.PlayReversed = PlayReversed;

            return playable;
        }
    }
}
