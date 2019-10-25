using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(menuName = "BFT/Profiles/Multi Parameter Profile", fileName = "Multi Profile Parameter")]
    public class MultiProfiledParameter : SerializedScriptableObject, IParametric
    {
        public List<ProfiledParameter> Parameters;

        private ProfiledParameter LastParameter => Parameters[Parameters.Count - 1];
        private ProfiledParameter FirstParameter => Parameters[0];

        public float Evaluate(float input)
        {
            foreach (var parameter in Parameters)
            {
                if ((input >= parameter.MinInput || parameter == FirstParameter)
                    && (input < parameter.MaxInput || parameter == LastParameter))
                {
                    return parameter.Evaluate(input);
                }
            }

            return 0;
        }
    }

    [Serializable]
    public class ProfiledParameter : IParametric
    {
        public float MaxInput;
        public float MaxOutput;
        public float MinInput;

        public float MinOutput;

        public AnimationCurve Profile;

        public float Evaluate(float input)
        {
            return MathExt.EvaluateCurve(MinOutput, MaxOutput, Profile, input, MinInput, MaxInput);
        }
    }
}
