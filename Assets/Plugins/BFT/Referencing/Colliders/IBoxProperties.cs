using UnityEngine;

namespace BFT
{
    public interface IBoxProperties
    {
        Vector3 Center { get; set; }
        Vector3 HalfSize { get; set; }
    }
}
