using UnityEngine;

namespace BFT
{
    [CreateAssetMenu(fileName = "NestedDB.asset", menuName = "Nested DB")]
    public class NestedDB : ScriptableObject
    {
        [SerializeField, ID("id")] public MainDict nested;
    }

    [System.Serializable]
    public class Example
    {
        public QueryTriggerInteraction enumVal;
        public string id;
        public NestedDict nestedData;
    }

    [System.Serializable]
    public class NestedExample
    {
        public Color color;
        public Nested2Dict deepNested;
        public GameObject prefab;
        public float speed;
    }

    [System.Serializable]
    public class MainDict : SerializableDictionaryBase<string, Example>
    {
    }

    [System.Serializable]
    public class NestedDict : SerializableDictionaryBase<int, NestedExample>
    {
    }

    [System.Serializable]
    public class Nested2Dict : SerializableDictionaryBase<QueryTriggerInteraction, string>
    {
    }
}
