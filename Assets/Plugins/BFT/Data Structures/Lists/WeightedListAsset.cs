using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace BFT
{
    public class WeightedListAsset<T> : SerializedScriptableObject, IVariable<T>
    {
        [NonSerialized] private bool initialized;

        private float totalCount;
        [OnValueChanged("RecalculateCount")] public List<WeightedValue> WeightedValues = new List<WeightedValue>();

        public T this[int key] => WeightedValues[key].Value;

        public virtual T Value
        {
            get => GetRandomElement();
            set =>
                WeightedValues.Add(new WeightedValue()
                {
                    Value = value,
                    Weight = 1
                });
        }

        private void RecalculateCount()
        {
            totalCount = 0;

            foreach (var value in WeightedValues)
            {
                totalCount += value.Weight;
            }
        }


        public int IndexOf(T value)
        {
            return WeightedValues.FindIndex(_ => _.Value.Equals(value));
        }

        public T GetRandomElement()
        {
            if (WeightedValues.Count == 0)
                return default;

            if (!initialized)
                Initialize();
            float picked = Random.Range(0, totalCount);
            float cumulated = 0;
            for (int i = 0; i < WeightedValues.Count; i++)
            {
                cumulated += WeightedValues[i].Weight;
                if (picked <= cumulated)
                    return WeightedValues[i].Value;
            }

            return WeightedValues.Last().Value;
        }


        [Button(ButtonSizes.Medium)]
        public void NormalizeWeights()
        {
            RecalculateCount();
            for (int i = 0; i < WeightedValues.Count; i++)
            {
                WeightedValues[i].Weight /= totalCount;
            }

            totalCount = 1;
            WeightedValues = WeightedValues.OrderByDescending(_ => _.Weight).ToList();
        }

        private void Initialize()
        {
            RecalculateCount();
            WeightedValues = WeightedValues.OrderByDescending(_ => _.Weight).ToList();
            initialized = true;
        }

        [Serializable]
        public class WeightedValue
        {
            public T Value;

            [MinValue(0)] public float Weight;
        }
    }
}
