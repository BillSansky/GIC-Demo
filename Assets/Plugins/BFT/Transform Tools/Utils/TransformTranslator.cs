using UnityEngine;

namespace BFT
{
    public class TransformTranslator : MonoBehaviour
    {
        public Vector3Value Speed;

        void Update()
        {
            transform.Translate(Speed.Value * UnityEngine.Time.deltaTime);
        }
    }
}
