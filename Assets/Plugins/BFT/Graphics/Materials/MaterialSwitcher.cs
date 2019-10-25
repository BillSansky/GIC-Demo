using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public class MaterialSwitcher : MonoBehaviour
    {
        private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();
        public Renderer[] renderers;
        public Material targetMaterial;

        private void Awake()
        {
            foreach (Renderer mr in renderers)
            {
                originalMaterials.Add(mr, mr.sharedMaterials);
            }
        }

        public void SwitchToTargetMaterial()
        {
            foreach (Renderer mr in renderers)
            {
                mr.sharedMaterials = new Material[] {targetMaterial};
            }
        }

        public void SwitchToOriginalMaterial()
        {
            foreach (Renderer mr in renderers)
            {
                if (mr && originalMaterials.ContainsKey(mr))
                    mr.sharedMaterials = originalMaterials[mr];
            }
        }
    }
}
