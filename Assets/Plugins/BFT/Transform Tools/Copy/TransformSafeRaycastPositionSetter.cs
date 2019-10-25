using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    /// <summary>
    ///     Move a transform based on a raycast direction, and only chose a sp
    /// </summary>
    public class TransformSafeRaycastPositionSetter : UpdateTypeSwitchable
    {
        [SerializeField] [FormerlySerializedAs("LocalSpacePivot")]
        private Vector3Value boundsLocalSpacePivot;

        public LayerMask CollisionCheckLayers;

        [FoldoutGroup("Utils")] public bool DebugLog;

        [FoldoutGroup("Utils")] public bool DrawRayTestPoints;

        [SerializeField] [FormerlySerializedAs("OffsetDistances")]
        private BoundsValue offsetBounds;

        public OrientationValue OffsetOrientation;

        private Vector3[] raycastDirections = new Vector3[6];
        private float[] raycastOffsets = new
            float[6];

        [SerializeField] private Vector3Value raycastOrigin;

        private Vector3Value raycastReverseDirection;
        public UnityEngine.Transform RaycastWantedPosition;

        public int SafePositionFindingIterationAmount = 6;

        public Vector3Value RaycastReverseDirection
        {
            get => raycastReverseDirection;
            set => raycastReverseDirection = value;
        }

        public Vector3Value RaycastOrigin
        {
            get => raycastOrigin;
            set => raycastOrigin = value;
        }

        public BoundsValue OffsetBounds
        {
            get => offsetBounds;
            set => offsetBounds = value;
        }

        public Vector3Value BoundsLocalSpacePivot
        {
            get => boundsLocalSpacePivot;
            set => boundsLocalSpacePivot = value;
        }

        public void SetOffsetBound(PropertyComponent obj)
        {
            OffsetBounds = (BoundsValue) obj.Data;
        }

        public void SetOffsetPivot(PropertyComponent pivot)
        {
            boundsLocalSpacePivot = (Vector3Value) pivot.Data;
        }

        public void SetOffsetOrientation(PropertyComponent orientation)
        {
            OffsetOrientation = (OrientationValue) orientation.Data;
        }

        public override void UpdateMethod()
        {
            if (!RaycastWantedPosition)
                return;

            Vector3 previousSafePoint = RaycastOrigin.Value;
            Vector3 midPoint = RaycastWantedPosition.position;

            bool foundCollision = false;

            transform.position = previousSafePoint;

            for (int i = 0; i < SafePositionFindingIterationAmount; i++)
            {
                if (foundCollision)
                {
                    if (DebugLog)
                    {
                        UnityEngine.Debug.Log(
                            $"iteration {i} collision previously found, centering between {previousSafePoint} and {midPoint}");
                    }

                    midPoint = MathExt.Center(midPoint, previousSafePoint);
                }
                else if (i > 0)
                {
                    if (i == 1)
                        break;

                    if (DebugLog)
                    {
                        UnityEngine.Debug.Log(
                            $"iteration {i} collision not found, centering between {midPoint} and {RaycastWantedPosition.position}");
                    }

                    previousSafePoint = midPoint;
                    midPoint = MathExt.Center(midPoint, RaycastWantedPosition.position);
                }

                Vector3 origin = midPoint + BoundsLocalSpacePivot.Value;

                raycastDirections[0] = OffsetOrientation.Forward;
                raycastDirections[1] = -OffsetOrientation.Forward;
                raycastDirections[2] = OffsetOrientation.Up;
                raycastDirections[3] = -OffsetOrientation.Up;
                raycastDirections[4] = OffsetOrientation.Right;
                raycastDirections[5] = -OffsetOrientation.Right;

                Vector3 pivotOffset = BoundsLocalSpacePivot.Value - OffsetBounds.Value.center;

                raycastOffsets[0] = pivotOffset.x + OffsetBounds.Value.extents.x;
                raycastOffsets[1] = -pivotOffset.x + OffsetBounds.Value.extents.x;
                raycastOffsets[2] = pivotOffset.y + OffsetBounds.Value.extents.y;
                raycastOffsets[3] = -pivotOffset.y + OffsetBounds.Value.extents.y;
                raycastOffsets[4] = pivotOffset.z + OffsetBounds.Value.extents.z;
                raycastOffsets[5] = -pivotOffset.z + OffsetBounds.Value.extents.z;

                foundCollision = false;

                for (int j = 0; j < 6; j++)
                {
                    if (DebugLog)
                    {
                        RaycastHit hit;
                        if (UnityEngine.Physics.Raycast(new Ray(origin, raycastDirections[j]), out hit,
                            raycastOffsets[j], CollisionCheckLayers))
                        {
                            if (DebugLog)
                                UnityEngine.Debug.Log($" a ray cast hit the collider {hit.collider.name}", hit.collider);
                            foundCollision = true;
                            break;
                        }
                    }

                    if (UnityEngine.Physics.Raycast(new Ray(origin, raycastDirections[j]),
                        raycastOffsets[j], CollisionCheckLayers))
                    {
                        foundCollision = true;
                        break;
                    }
                }
            }

            transform.position = foundCollision ? previousSafePoint : midPoint;
        }

        public void OnDrawGizmosSelected()
        {
            if (!RaycastWantedPosition || OffsetOrientation == null)
                return;

            raycastDirections[0] = OffsetOrientation.Forward;
            raycastDirections[1] = -OffsetOrientation.Forward;
            raycastDirections[2] = OffsetOrientation.Up;
            raycastDirections[3] = -OffsetOrientation.Up;
            raycastDirections[4] = OffsetOrientation.Right;
            raycastDirections[5] = -OffsetOrientation.Right;

            Vector3 pivotOffset = BoundsLocalSpacePivot.Value - OffsetBounds.Value.center;

            raycastOffsets[0] = pivotOffset.x + OffsetBounds.Value.extents.x;
            raycastOffsets[1] = -pivotOffset.x + OffsetBounds.Value.extents.x;
            raycastOffsets[2] = pivotOffset.y + OffsetBounds.Value.extents.y;
            raycastOffsets[3] = -pivotOffset.y + OffsetBounds.Value.extents.y;
            raycastOffsets[4] = pivotOffset.z + OffsetBounds.Value.extents.z;
            raycastOffsets[5] = -pivotOffset.z + OffsetBounds.Value.extents.z;

            Vector3 midPoint = RaycastWantedPosition.position;


            Color[] colors = new[]
            {
                Color.blue, Color.blue.DarkerBrighter(-0.1f),
                Color.green, Color.green.DarkerBrighter(-0.1f),
                Color.red, Color.red.DarkerBrighter(-0.1f)
            };

            for (int j = 0; j < 6; j++)
            {
                Gizmos.color = colors[j];
                Gizmos.DrawLine(midPoint, midPoint + raycastDirections[j] * raycastOffsets[j]);
            }

            if (!DrawRayTestPoints)
                return;

            List<Vector3> safePointsPos = new List<Vector3>();
            List<Vector3> unsafePointsPos = new List<Vector3>();

            Vector3 previousSafePoint = RaycastOrigin.Value;

            bool foundCollision = false;

            for (int i = 0; i < SafePositionFindingIterationAmount; i++)
            {
                if (foundCollision)
                {
                    unsafePointsPos.Add(midPoint);
                    midPoint = MathExt.Center(midPoint, previousSafePoint);
                }
                else if (i > 0)
                {
                    if (i == 1)
                        break;

                    safePointsPos.Add(midPoint);

                    previousSafePoint = midPoint;
                    midPoint = MathExt.Center(midPoint, RaycastWantedPosition.position);
                }

                Vector3 origin = midPoint + BoundsLocalSpacePivot.Value;

                raycastDirections[0] = OffsetOrientation.Forward;
                raycastDirections[1] = -OffsetOrientation.Forward;
                raycastDirections[2] = OffsetOrientation.Up;
                raycastDirections[3] = -OffsetOrientation.Up;
                raycastDirections[4] = OffsetOrientation.Right;
                raycastDirections[5] = -OffsetOrientation.Right;


                raycastOffsets[0] = pivotOffset.x + OffsetBounds.Value.extents.x;
                raycastOffsets[1] = -pivotOffset.x + OffsetBounds.Value.extents.x;
                raycastOffsets[2] = pivotOffset.y + OffsetBounds.Value.extents.y;
                raycastOffsets[3] = -pivotOffset.y + OffsetBounds.Value.extents.y;
                raycastOffsets[4] = pivotOffset.z + OffsetBounds.Value.extents.z;
                raycastOffsets[5] = -pivotOffset.z + OffsetBounds.Value.extents.z;

                foundCollision = false;

                for (int j = 0; j < 6; j++)
                {
                    if (DebugLog)
                    {
                        RaycastHit hit;
                        if (UnityEngine.Physics.Raycast(new Ray(origin, raycastDirections[j]), out hit,
                            raycastOffsets[j], CollisionCheckLayers))
                        {
                            foundCollision = true;
                            break;
                        }
                    }

                    if (UnityEngine.Physics.Raycast(new Ray(origin, raycastDirections[j]),
                        raycastOffsets[j], CollisionCheckLayers))
                    {
                        foundCollision = true;
                        break;
                    }
                }
            }

            Gizmos.color = Color.green;
            int k = 0;

            foreach (var safePointsPo in safePointsPos)
            {
                Gizmos.DrawWireSphere(safePointsPo, 0.1f);
                HandleExt.Text(safePointsPo + Vector3.up * 0.02f, k.ToString(), true);
                k++;
            }

            Gizmos.color = Color.red;

            k = 0;

            foreach (var unsafePointsPo in unsafePointsPos)
            {
                Gizmos.DrawWireSphere(unsafePointsPo, 0.1f);
                HandleExt.Text(unsafePointsPo + Vector3.up * 0.02f, k.ToString(), true);
                k++;
            }
        }
    }
}
