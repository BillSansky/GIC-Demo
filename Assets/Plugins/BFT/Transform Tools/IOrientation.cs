using UnityEngine;

namespace BFT
{
    public interface IOrientation
    {
        Vector3 Up { get; }
        Vector3 Right { get; }

        Vector3 Forward { get; }
    }
}
