using UnityEngine;

namespace BFT
{
    public interface ICapsuleProperties
    {
        float Height { get; set; }
        float Radius { get; set; }
        Vector3 Center { get; set; }
    }
}
