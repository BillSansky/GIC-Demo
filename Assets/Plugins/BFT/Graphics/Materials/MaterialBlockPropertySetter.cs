using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    [ExecuteAlways]
    public class MaterialBlockPropertySetter : MonoBehaviour
    {
        [BoxGroup("Block")] public MaterialPropertyBlock block;

        [BoxGroup("Color")] [ShowIf("UseColorComponent")] [OnValueChanged("SetColorProperty")]
        public ColorValue Color;

        private int colorPropertyID = -1;

        [ValueDropdown("ColorProperties")] [BoxGroup("Color")]
        public string ColorPropertyName = "";

        public Renderer Renderer;

        [BoxGroup("Color")] public bool SetColorPropertyOnEnable = true;

        [BoxGroup("Texture")] public bool SetTexturePropertyOnEnable = true;

        private int texturePropertyID = -1;

        [ValueDropdown("TextureProperties")] [BoxGroup("Texture")]
        public string TexturePropertyName = "";

        [BoxGroup("Texture")] [OnValueChanged("SetTextureProperty")]
        public Texture TextureToSet;

#if UNITY_EDITOR
        private ValueDropdownList<string> TextureProperties =>
            ShaderUtilities.ShaderTexturePropertyNames(Renderer.sharedMaterial);
#endif

#if UNITY_EDITOR
        private ValueDropdownList<string> ColorProperties =>
            ShaderUtilities.ShaderColorPropertyNames(Renderer.sharedMaterial);
#endif

        public void Reset()
        {
            Renderer = GetComponent<Renderer>();
        }

        public void OnEnable()
        {
            GenerateIDs();

            if (SetTexturePropertyOnEnable)
                SetTextureProperty();
            if (SetColorPropertyOnEnable)
                SetColorProperty();
        }

        private void GenerateIDs()
        {
            texturePropertyID = Shader.PropertyToID(TexturePropertyName);
            colorPropertyID = Shader.PropertyToID(ColorPropertyName);
        }

        [Button(ButtonSizes.Medium)]
        public void SetTextureProperty()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GenerateIDs();
            }
#endif

            var enableRenderer = TextureToSet != null;
            if (Application.isPlaying && Renderer != null && enableRenderer != Renderer.enabled)
            {
                Renderer.enabled = enableRenderer;
            }

            if (!TextureToSet)
            {
                return;
            }

            if (block == null)
                block = new MaterialPropertyBlock();

            block.SetTexture(texturePropertyID, TextureToSet);
            Renderer.SetPropertyBlock(block);
        }

        [Button(ButtonSizes.Medium)]
        public void SetColorProperty()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GenerateIDs();
            }
#endif

            if (!Renderer)
                return;

            if (block == null)
                block = new MaterialPropertyBlock();
            block.SetColor(colorPropertyID, Color.Value);

            Renderer.SetPropertyBlock(block);
        }

        [Button(ButtonSizes.Medium)]
        public void RemoveProperties()
        {
            block.Clear();
            Renderer.SetPropertyBlock(block);
        }
    }
}
