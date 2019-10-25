using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{

    public class TransformMaxDistanceConstrain : AbstractTransformConstraint, IValue<bool>
    {
        [BoxGroup("Utils")] public bool ConstrainInEditMode;

        [BoxGroup("Status"), ShowInInspector, ReadOnly]
        private bool isConstrained;

        [BoxGroup("Utils")] public bool LogDebug;

        [BoxGroup("Constraints")] [HideIf("UseNestedPrefab")]
        public FloatValue MaxDistance;

        [ShowIf("UseNestedPrefab")] [BoxGroup("Constraints")]
        public float MaxDistanceValue;

        [BoxGroup("Constraints")] public TransformValue Reference;

        [BoxGroup("Constraints")] public bool UseNestedPrefab;

        public bool Value => !isConstrained;

        public void Awake()
        {
            if (UseNestedPrefab)
            {
                MaxDistance = new FloatValue() {LocalValue = MaxDistanceValue};
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
#if UNITY_EDITOR
            if (!Reference.Value || MaxDistance == null)
            {
                UnityEngine.Debug.LogWarningFormat(this,
                    "The reference transform or the distance are missing on {0}, disabling the component",
                    gameObject.name);
                enabled = false;
                return;
            }
#endif
        }

        public override void Constrain()
        {
#if UNITY_EDITOR
            if (!Reference.Value || MaxDistance == null)
            {
                UnityEngine.Debug.LogWarningFormat(this,
                    "The reference transform or the distance are missing on {0}, disabling the component",
                    gameObject.name);
                enabled = false;
                return;
            }
#endif

            isConstrained = false;
            Vector3 distance = (transform.position - Reference.position);
            if (distance.magnitude > MaxDistance.Value)
            {
                isConstrained = true;

                if (LogDebug)
                {
                    UnityEngine.Debug.LogFormat(this, "Transform {0} Constrained", name);
                }

                distance = Vector3.ClampMagnitude(distance, MaxDistance.Value);
                transform.position = Reference.position + distance;
            }
        }

        [BoxGroup("Utils")]
        [Button(ButtonSizes.Medium)]
        public void SetConstrainToCurrentDistance()
        {
            if (!Reference.Value)
            {
                return;
            }

            float distance = (transform.position - Reference.position).magnitude;
            if (UseNestedPrefab)
            {
                MaxDistanceValue = distance;
            }
            else
            {
                MaxDistance.LocalValue = distance;
            }
        }

#if UNITY_EDITOR

        [BoxGroup("Utils")] public bool ShowGizmos;

        public void OnDrawGizmosSelected()
        {
            if (!enabled || !ShowGizmos || !Reference.Value || MaxDistance == null || !ConstrainInEditMode)
            {
                return;
            }

            Gizmos.color = Color.cyan.Alphaed(.2f);
            Gizmos.DrawSphere(Reference.position, MaxDistance.Value);
            HandleExt.Text(transform.position + Vector3.forward * (MaxDistance.Value + 1), "Clamping Distance", true);

            if (!Application.isPlaying)
            {
                Constrain();
            }
        }

        public void OnDrawGizmos()
        {
            if (enabled && !Application.isPlaying && ConstrainInEditMode)
            {
                Constrain();
            }
        }
#endif
    }
}


/*
public class TransformChainMaxDistanceConstrain : AbstractTransformConstraint, IGenericValue<bool>
{
    [BoxGroup("Constraints")]
    public List<Transform> ChainTransformsBefore;
    public List<Transform> ChainTransformsAfter;

    [BoxGroup("Constraints")]
    public IGenericValue<float> MaxDistanceBetweenChainElements;

    [BoxGroup("Status"), ShowInInspector, ReadOnly]
    private bool isConstrained;

    [BoxGroup("Utils")]
    public bool ConstrainInEditMode;

    [BoxGroup("Utils")]
    public bool LogDebug;

    public override void OnEnable()
    {
        base.OnEnable();
#if UNITY_EDITOR
        if (!ChainTransformsBefore || MaxDistanceBetweenChainElements == null)
        {
            Debug.LogWarningFormat(this, "The reference transform or the distance are missing on {0}, disabling the component", gameObject.name);
            enabled = false;
            return;
        }
#endif

    }

    public override void Constrain()
    {
#if UNITY_EDITOR
        if (!ChainTransformsBefore || MaxDistanceBetweenChainElements == null)
        {
            Debug.LogWarningFormat(this, "The reference transform or the distance are missing on {0}, disabling the component", gameObject.name);
            enabled = false;
            return;
        }
#endif

        isConstrained = false;
        Vector3 distance = (transform.position - ChainTransformsBefore.position);
        if (distance.magnitude > MaxDistanceBetweenChainElements.Value)
        {
            isConstrained = true;

            if (LogDebug)
                Debug.LogFormat(this, "Transform {0} Constrained", name);

            distance = Vector3.ClampMagnitude(distance, MaxDistanceBetweenChainElements.Value);
            transform.position = ChainTransformsBefore.position + distance;
        }
    }

    public bool Value => !isConstrained;

#if UNITY_EDITOR

    [BoxGroup("Utils")]
    public bool ShowGizmos;

    public void OnDrawGizmosSelected()
    {

        if (!enabled || !ShowGizmos || !ChainTransformsBefore || MaxDistanceBetweenChainElements == null || !ConstrainInEditMode)
        {
            return;
        }

        Gizmos.color = Color.cyan.Alphaed(.2f);
        Gizmos.DrawSphere(ChainTransformsBefore.position, MaxDistanceBetweenChainElements.Value);
        HandleExt.Text(transform.position + Vector3.forward * (MaxDistanceBetweenChainElements.Value + 1), "Clamping Distance", true);

        if (!Application.isPlaying)
        {
            Constrain();
        }
    }

    public void OnDrawGizmos()
    {
        if (enabled && !Application.isPlaying && ConstrainInEditMode)
        {
            Constrain();
        }
    }
#endif

}*/
