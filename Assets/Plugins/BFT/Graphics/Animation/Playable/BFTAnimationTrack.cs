using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace BFT
{
    [TrackClipType(typeof(BFTAnimationControlAsset))]
    [TrackBindingType(typeof(TimelineBFTAnimation))]
    public class BFTAnimationTrack : TrackAsset
    {
        public override UnityEngine.Playables.Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            // before building, update the binding field in the clips assets;
            var director = go.GetComponent<PlayableDirector>();
            var binding = director.GetGenericBinding(this);

            foreach (var c in GetClips())
            {
                var myAsset = c.asset as BFTAnimationControlAsset;
                if (myAsset != null)
                    myAsset.Target = (IAnimation) binding;
            }

            return base.CreateTrackMixer(graph, go, inputCount);
        }

        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            base.GatherProperties(director, driver);
        }
    }
}
