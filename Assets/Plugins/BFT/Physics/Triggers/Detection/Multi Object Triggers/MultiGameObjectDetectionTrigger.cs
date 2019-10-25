using System.Collections.Generic;
using UnityEngine;

namespace Plugins.BFT.Physics.Triggers.Detection.Multi_Object_Triggers
{
    public class MultiGameObjectDetectionTrigger : MultiDetectionTrigger<GameObject>
    {
        public override List<GameObject> LastDetectedObject
        {
            get => CurrentObjectDetected;
            protected set
            {
                //do nothing
            }
        }
    }
}