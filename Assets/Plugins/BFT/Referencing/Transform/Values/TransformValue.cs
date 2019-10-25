using System;
using UnityEngine;

// ReSharper disable InconsistentNaming Unity name FTW

namespace BFT
{
    [Serializable]
    public class TransformValue : GenericValue<UnityEngine.Transform>
    {
        public Vector3 position
        {
            get => LocalValueInspected.position;
            set => LocalValueInspected.position = value;
        }

        public Vector3 localPosition
        {
            get => LocalValueInspected.localPosition;
            set => LocalValueInspected.localPosition = value;
        }

        public Vector3 eulerAngles
        {
            get => LocalValueInspected.eulerAngles;
            set => LocalValueInspected.eulerAngles = value;
        }

        public Vector3 localEulerAngles
        {
            get => LocalValueInspected.localEulerAngles;
            set => LocalValueInspected.localEulerAngles = value;
        }

        public Vector3 right
        {
            get => LocalValueInspected.right;
            set => LocalValueInspected.right = value;
        }

        public Vector3 up
        {
            get => LocalValueInspected.up;
            set => LocalValueInspected.up = value;
        }

        public Vector3 forward
        {
            get => LocalValueInspected.forward;
            set => LocalValueInspected.forward = value;
        }

        public UnityEngine.Quaternion rotation
        {
            get => LocalValueInspected.rotation;
            set => LocalValueInspected.rotation = value;
        }

        public UnityEngine.Quaternion localRotation
        {
            get => LocalValueInspected.localRotation;
            set => LocalValueInspected.localRotation = value;
        }

        public Vector3 localScale
        {
            get => LocalValueInspected.localScale;
            set => LocalValueInspected.localScale = value;
        }

        public UnityEngine.Transform parent
        {
            get => LocalValueInspected.parent;
            set => LocalValueInspected.parent = value;
        }
    }
}
