using System;
using Sirenix.OdinInspector;

namespace BFT
{
    [Serializable]
    public class ColorValue : GenericValue<UnityEngine.Color>
    {
        [ColorUsageProperty(true, true)]
        [HorizontalGroup]
        [HideIf("UseReference"), HideLabel, ShowInInspector]
        protected override UnityEngine.Color LocalValueInspected
        {
            get => LocalValue;
            set => LocalValue = value;
        }
    }
}
