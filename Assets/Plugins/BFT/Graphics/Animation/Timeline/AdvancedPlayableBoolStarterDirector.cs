using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace BFT
{
    [RequireComponent(typeof(PlayableDirector))]
    public class AdvancedPlayableBoolStarterDirector : AdvancedPlayableDirector
    {
        public BoolVariableAsset BoolValue;

        [AssetsOnly] public PlayableAsset OnActivationAsset;

        [AssetsOnly] public PlayableAsset OnDeactivationAsset;

        protected void OnEnable()
        {
            BoolValue.OnValueChanged.RemoveListener(CheckActivation);
            BoolValue.OnValueChanged.AddListener(CheckActivation);
        }

        private void CheckActivation()
        {
            if (BoolValue.Value)
                Activate();
            else
            {
                Deactivate();
            }
        }

        void OnDisable()
        {
            BoolValue.OnValueChanged.RemoveListener(CheckActivation);
        }

        private void Activate()
        {
            StartDirector(OnActivationAsset);
        }

        private void Deactivate()
        {
            StartDirector(OnDeactivationAsset);
        }

        private void StartDirector(PlayableAsset asset)
        {
            Director.Stop();
            Director.time = 0;
            Director.playableAsset = asset;
            Play();
        }
    }
}
