using UnityEngine;

namespace BFT
{

    /// <summary>
    /// Prevents a transform from escaping defined corners
    /// </summary>
    public class TransformRectConstrainer : AbstractTransformConstraint
    {
        private readonly Vector3Value[] corners = new Vector3Value[4];

        public RectTransform Constrainer;
        public bool ConstrainZ;

        private Vector3[] cornersCalculated=new Vector3[4];
        
        public override void Constrain()
        {
            for (var index = 0; index < corners.Length; index++)
            {
                var corner = corners[index];
                cornersCalculated[index] = corner.Value;
            }

            Vector3 position = Constrainer.InverseTransformPoint(transform.position);
            Constrainer.GetLocalCorners(cornersCalculated);

            position.x = Mathf.Clamp(position.x, cornersCalculated[0].x, cornersCalculated[2].x);
            position.y = Mathf.Clamp(position.y, cornersCalculated[0].y, cornersCalculated[1].y);
            if (ConstrainZ)
                position.z = 0;

            transform.position = Constrainer.TransformPoint(position);
        }
    }
}
