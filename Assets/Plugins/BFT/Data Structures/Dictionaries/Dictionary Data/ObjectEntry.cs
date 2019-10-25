using UnityEngine;

namespace BFT
{
    public abstract class ObjectEntry<T> : DictionaryEntry<T> where T : UnityEngine.Object
    {
        [SerializeField] private T field;

        public override string NameID
        {
            get => field.name;

            set => field.name = value;
        }

        public override T Data
        {
            get => field;

            set => field = value;
        }
    }
}
