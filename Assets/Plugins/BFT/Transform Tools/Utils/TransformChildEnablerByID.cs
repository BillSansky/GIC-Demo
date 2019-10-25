using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Enables only the first X amount of objects
    /// </summary>
    public class TransformChildEnablerByID : MonoBehaviour
    {
        public IntValue numberOfEnabledChildren;
        private int? prevValue;

        private void LateUpdate()
        {
            if (!prevValue.HasValue || prevValue != numberOfEnabledChildren.Value)
            {
                EnableChildren();
                prevValue = numberOfEnabledChildren.Value;
            }
        }

        private void EnableChildren()
        {
            int childrenCount = transform.childCount;
            for (int i = 0; i < childrenCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i < numberOfEnabledChildren.Value);
            }
        }
    }
}
