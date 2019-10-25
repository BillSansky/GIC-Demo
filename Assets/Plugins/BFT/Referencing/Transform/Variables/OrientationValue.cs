using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [Serializable]
    public class OrientationValue : GenericValue<IOrientation>, IOrientation
    {
        [InfoBox("Uses global axes in value mode", VisibleIf = "UseValue", GUIAlwaysEnabled = true)]
        public OrientationValue()
        {
            UseReference = true;
        }

        private bool UseValue => !UseReference;


        public override IOrientation Value =>
            !UseReference ? this : (!Reference ? default : ((IValue<IOrientation>) Reference).Value);

        public Vector3 Up => Vector3.up;
        public Vector3 Right => Vector3.right;
        public Vector3 Forward => Vector3.forward;
    }


    [Serializable]
    public class OrientationVariable : GenericVariable<IOrientation>
    {
    }
}
