using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshMerger : MonoBehaviour
{
    public GameObject MergeParent;

    public void MergeSubMeshes()
    {
        MergeMeshes();
        MeshFilter[] meshFilters = MergeParent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        MergeParent.transform.GetComponent<MeshFilter>().sharedMesh = new Mesh();
        MergeParent.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
    }

    public void MergeMeshes()
    {
        Matrix4x4 myTransform = MergeParent.transform.worldToLocalMatrix;
        Dictionary<Material, List<CombineInstance>> combines = new Dictionary<Material, List<CombineInstance>>();
        MeshRenderer[] meshRenderers = MergeParent.GetComponentsInChildren<MeshRenderer>();

        foreach (var meshRenderer in meshRenderers)
        {
            foreach (var material in meshRenderer.sharedMaterials)
                if (material != null && !combines.ContainsKey(material))
                    combines.Add(material, new List<CombineInstance>());
        }

        MeshFilter[] meshFilters = MergeParent.GetComponentsInChildren<MeshFilter>();
        foreach (var filter in meshFilters)
        {
            if (filter.sharedMesh == null)
                continue;
            CombineInstance ci = new CombineInstance();
            ci.mesh = filter.sharedMesh;

            ci.transform = myTransform * filter.transform.localToWorldMatrix;
            combines[filter.GetComponent<MeshRenderer>().sharedMaterial].Add(ci);
            filter.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (Material m in combines.Keys)
        {
            var go = new GameObject("Combined mesh");
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            var filter = go.AddComponent<MeshFilter>();
            filter.sharedMesh.CombineMeshes(combines[m].ToArray(), true, true);

            var rrenderer = go.AddComponent<MeshRenderer>();
            rrenderer.material = m;
        }
    }
}