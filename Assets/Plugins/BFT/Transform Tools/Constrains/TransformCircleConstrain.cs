using UnityEditor;
using UnityEngine;

namespace BFT
{

    public enum ECircleConstrainUpOrientation
    {
        ABSOLUTE_UP,
        REFERENCE_UP
    }


    public class TransformCircleConstrain : AbstractTransformConstraint
    {
        public ECircleConstrainUpOrientation CircleUp;

        public bool CircumferenceOnly;
        public float Radius;
        public UnityEngine.Transform Reference;
        public UnityEngine.Transform ToConstrain;

        private void Awake()
        {
            ToConstrain = transform;
        }

        public override void Constrain()
        {
            Vector3 pos = transform.position;
            Vector3 refPos = Reference.position;
            Vector3 newPos = pos;

            switch (CircleUp)
            {
                case ECircleConstrainUpOrientation.ABSOLUTE_UP:
                    newPos = MathExt.ProjectPointOnPlane(Vector3.up, Reference.position, pos);
                    break;
                case ECircleConstrainUpOrientation.REFERENCE_UP:
                    newPos = MathExt.ProjectPointOnPlane(Reference.up, Reference.position, pos);
                    break;
            }


            Vector3 distance = (newPos - refPos);
            if (distance.magnitude > Radius)
            {
                distance = Vector3.ClampMagnitude(distance, Radius);
                newPos = refPos + distance;
                ToConstrain.position = newPos;
            }
            else if
                (CircumferenceOnly && distance.magnitude < Radius)
            {
                if (distance == Vector3.zero)
                    distance = Reference.forward;
                distance = distance.normalized * Radius;

                newPos = refPos + distance;
                ToConstrain.position = newPos;
            }
        }

#if UNITY_EDITOR
        public Color DebugColor = new Color(1, 0, 0, 0.8f);
        public bool AlwaysShow = false;

        private void OnDrawGizmosSelected()
        {
            if (!AlwaysShow)
                DrawDebug();
        }

        private void OnDrawGizmos()
        {
            if (AlwaysShow)
                DrawDebug();

            if (!Application.isPlaying)
                Constrain();
        }

        private void DrawDebug()
        {
            if (!Reference)
                return;
            Handles.color = DebugColor;
            Handles.matrix = Reference.localToWorldMatrix;
            if (!CircumferenceOnly)
                Handles.DrawSolidDisc(Vector3.zero, Vector3.up, Radius);
            else
            {
                Handles.DrawWireDisc(Vector3.zero, Vector3.up, Radius);
            }
        }
#endif
    }
}
