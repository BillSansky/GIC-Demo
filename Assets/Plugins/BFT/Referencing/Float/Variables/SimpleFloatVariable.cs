using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     useful for usage in animations
    /// </summary>
    public class SimpleFloatVariable : MonoBehaviour, IVariable<float>
    {
        [SerializeField] private float value;

        public float Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
