using UnityEngine;
using System.Collections;

public class PhysicsRemover : MonoBehaviour
{
    public GameObject colliderContainer;
    public bool colliderOnly;

    public void RemovePhysics()
    {
        Collider[] cols = colliderContainer.GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            DestroyImmediate(col);
        }

        if (!colliderOnly)
        {
            Rigidbody[] rbs = colliderContainer.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                DestroyImmediate(rb);
            }
        }
    }
}