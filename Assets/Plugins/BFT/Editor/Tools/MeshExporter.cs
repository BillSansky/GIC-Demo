using System.IO;
using UnityEditor;
using UnityEngine;

namespace BFT
{
    public class MeshExporter
    {
        ///////////////////////
        ///////////////////////
        ///////////////////////
        public static void ExportMesh(GameObject go, string projectPath, bool exportChildren)
        {
            MeshFilter mf = (MeshFilter) go.GetComponent<MeshFilter>();
            Mesh mesh = null;

            if (mf)
            {
                mesh = mf.sharedMesh;
            }

            if (mesh != null)
            {
                string assetPath = Path.Combine(projectPath, go.name + ".asset");

                AssetDatabase.DeleteAsset(assetPath); // Overwrite the asset by deleting any existing asset first
                AssetDatabase.CreateAsset(mesh, assetPath);

                // Reload the mesh from the asset database & link it back to the meshfilter
                mesh = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Mesh)) as Mesh;
                if (mesh)
                {
                    mf.sharedMesh = mesh;
                }
            }

            if (exportChildren)
            {
                foreach (Transform t in go.transform)
                {
                    ExportMesh(t.gameObject, projectPath, exportChildren);
                }
            }
        }
    }
}
