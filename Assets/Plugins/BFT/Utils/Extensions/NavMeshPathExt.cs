using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BFT
{
    public static class NavMeshPathExt
    {
        public static void GetDividedCornersNonAlloc(this NavMeshPath navMeshPath, NavMeshQueryFilter filter,
            float distanceDivider, float yOffset, List<Vector3> outputPath, Vector3[] pathBuffer, int pathCount)
        {
            outputPath.Clear();
            NavMeshHit hit;
            if (pathCount > 0)
            {
                for (int i = 0; i < pathCount - 1; i++)
                {
                    pathBuffer[i].y += yOffset;
                    outputPath.Add(pathBuffer[i]);
                    Vector3 direction = pathBuffer[i + 1] - pathBuffer[i];
                    float distance = Vector3.Magnitude(direction);
                    int additionalPoints = (int) (distance / distanceDivider);
                    if (additionalPoints > 0)
                    {
                        direction /= (additionalPoints + 1);
                        for (int j = 1; j <= additionalPoints; j++)
                        {
                            NavMesh.SamplePosition(pathBuffer[i] + direction * j, out hit, 5, filter);
                            if (hit.hit)
                            {
                                Vector3 additionalPosition = hit.position;
                                additionalPosition.y += yOffset;
                                outputPath.Add(additionalPosition);
                            }
                        }
                    }
                }

                pathBuffer[pathCount - 1].y += yOffset;
                outputPath.Add(pathBuffer[pathCount - 1]);
            }
        }


        public static void GetDividedCornersNonAlloc(this NavMeshPath navMeshPath, NavMeshQueryFilter filter,
            float distanceDivider, float yOffset, List<Vector3> outputPath, Vector3[] pathBuffer)
        {
            int count = navMeshPath.GetCornersNonAlloc(pathBuffer);
            navMeshPath.GetDividedCornersNonAlloc(filter, distanceDivider, yOffset, outputPath, pathBuffer, count);
        }
    }
}
