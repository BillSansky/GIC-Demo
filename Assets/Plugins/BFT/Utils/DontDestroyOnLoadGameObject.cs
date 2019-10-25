using UnityEngine;

namespace BFT
{
    public class DontDestroyOnLoadGameObject : MonoBehaviour
    {
        void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
