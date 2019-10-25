using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class TransformRayCastScreenToWorldSetter : MonoBehaviour
    {
        public CameraValue Cam;

        public LayerMask Mask;

        [ShowIf("IsOffset")] public bool OffsetRelativeToDirectionOfRay;

        public Vector3Value PositionOffset;

        [ShowIf("ResetPositionOnNoHit"), Indent]
        public TransformValue PositionOnNoHit;

        public float RaycastDistance = 9999;

        public bool RaycastIgnoreTrigger = true;

        public bool ResetPositionOnNoHit;

        public RectTransform ScreenPosition;

        public float TimeBetweenRaycasts = 0;

        public TransformValue ToRayCast;

        public bool IsOffset => PositionOffset.Value.magnitude > 0;


        void Reset()
        {
            ToRayCast.LocalValue = transform;
        }

        public void OnEnable()
        {
            StartCoroutine(RegularRaycast());
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator RegularRaycast()
        {
            yield return CoroutineUtils.CallRegularly(TimeBetweenRaycasts,
                RaycastPosition);
        }


        void RaycastPosition()
        {
            Ray ray = Cam.Value.ScreenPointToRay(ScreenPosition.position);
            RaycastHit hit;
            bool gotHit = UnityEngine.Physics.Raycast(ray, out hit, RaycastDistance, Mask,
                RaycastIgnoreTrigger ? QueryTriggerInteraction.Ignore : QueryTriggerInteraction.Collide);

            if (gotHit)
                ToRayCast.position = hit.point + PositionOffset.Value;
            else
            {
                if (ResetPositionOnNoHit)
                    ToRayCast.position = PositionOnNoHit.position;
            }
        }
    }
}
