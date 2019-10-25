using Sirenix.OdinInspector;

#if BFT_TEXTMESHPRO
using UnityEngine;
using TMPro;
namespace BFT
{
    public class UIFontMaterialColorUpdater : SerializedMonoBehaviour
    {
        public IValue<Color> Color;
        public string PropertyName;

        //note that this class does not use material property blocks to be usable in the canvas renderer

        public TextMeshProUGUI Target;

        public void UpdateMaterial()
        {
            UpdateMaterial(Target.fontSharedMaterial);
        }

        protected virtual void UpdateMaterial(Material mat)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                Target.fontSharedMaterial.SetColor(PropertyName, Color.Value);
                return;
            }
#endif
            if (mat)
            {
                mat.SetColor(PropertyName, Color.Value);
            }
        }
    }
}
#endif
