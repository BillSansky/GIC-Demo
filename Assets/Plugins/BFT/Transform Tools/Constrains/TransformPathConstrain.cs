using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
#endif

namespace BFT
{
    /// <summary>
    /// constrains a transform so it stays on a path
    /// </summary>
    public class TransformPathConstrain : AbstractTransformConstraint, IValue<bool>
    {
        public bool ClearOnDisable = true;

        [ShowIf("SetOffsetToLineRenderers")] public Vector3 LineRenderersOffset;

        public IValue<float> MaxCost;

        [SerializeField] private Transform transformToConstrain;

        public UnityEvent OnClear, OnActivate;

        private List<Vector3> pathAvailableTMP, pathUnavailableTMP;
        public Vector3ListValue pathGiver;
        private float prevMaxCost;
        public float CurrentlyUsedCost { get; private set; }


        public Vector3[] PathAvailable { private set; get; }
        public Vector3[] PathUnavailable { private set; get; }

        public bool Value => PathAvailable != null && PathAvailable.Length > 0;

        private void Reset()
        {
            transformToConstrain = transform;
        }

        private void Awake()
        {
            transformToConstrain = transform;
            pathAvailableTMP = new List<Vector3>(50);
            pathUnavailableTMP = new List<Vector3>(50);
        }


        public override void OnDisable()
        {
            base.OnDisable();
            if (ClearOnDisable)
            {
                Clear();
            }
        }

        private void Clear()
        {
            if (PathAvailable != null || PathUnavailable != null)
            {
                OnClear.ExtendedInvoke(this);
            }

            PathAvailable = null;
            PathUnavailable = null;
            pathAvailableTMP.Clear();
            pathUnavailableTMP.Clear();
            CurrentlyUsedCost = 0;
        }

        public override void Constrain()
        {
            if (pathGiver == null || pathGiver == null || pathGiver.Count == 0)
            {
                Clear();
                return;
            }


            prevMaxCost = MaxCost.Value;

            if (PathAvailable == null && PathUnavailable == null)
                OnActivate.ExtendedInvoke(this);

            pathAvailableTMP.Clear();
            pathUnavailableTMP.Clear();

            float currentCost = 0;
            if (pathGiver.Count == 1)
            {
                pathAvailableTMP.Add(pathGiver[0]);
            }
            else
            {
                for (int i = 1; i < pathGiver.Count; i++)
                {
                    float prevCost = currentCost;
                    Vector3 distance = pathGiver[i] - pathGiver[i - 1];
                    float cost = distance.magnitude;
                    currentCost += cost;

                    if (prevCost <= MaxCost.Value)
                    {
                        pathAvailableTMP.Add(pathGiver[i - 1]);

                        if (currentCost > MaxCost.Value)
                        {
                            float availableCost = cost - (currentCost - MaxCost.Value);
                            Vector3 clampedPos =
                                pathGiver[i - 1] + Vector3.ClampMagnitude(distance, availableCost);

                            pathAvailableTMP.Add(clampedPos);
                            pathUnavailableTMP.Add(clampedPos);
                        }
                    }
                    else
                    {
                        pathUnavailableTMP.Add(pathGiver[i - 1]);
                    }

                    if (i == pathGiver.Count - 1)
                    {
                        if (currentCost <= MaxCost.Value)
                            pathAvailableTMP.Add(pathGiver[i]);
                        else
                            pathUnavailableTMP.Add(pathGiver[i]);
                    }
                }


                CurrentlyUsedCost = Mathf.Min(MaxCost.Value, currentCost);
                PathAvailable = pathAvailableTMP.ToArray();
                PathUnavailable = pathUnavailableTMP.ToArray();

                Vector3 newPos = PathAvailable.Length > 0
                    ? PathAvailable[PathAvailable.Length - 1]
                    : PathUnavailable[0];
                //if (IgnoreYAxis)
                //    newPos.y = myTransform.position.y;

                transformToConstrain.position = newPos;
            }
        }


#if UNITY_EDITOR
        public bool ShowDebug;

        private void OnDrawGizmos()
        {
            if (ShowDebug)
            {
                if (PathAvailable != null && PathAvailable.Length > 0)
                {
                    Handles.color = Color.green;
                    Handles.DrawPolyLine(PathAvailable);
                }

                if (PathUnavailable != null && PathUnavailable.Length > 0)
                {
                    Handles.color = Color.red;
                    Handles.DrawPolyLine(PathUnavailable);
                }
            }
        }
#endif
    }
}
