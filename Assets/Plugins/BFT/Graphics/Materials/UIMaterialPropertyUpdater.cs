using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public enum MaterialPropertyType
    {
        FLOAT,
        COLOR,
        TEXTURE,
        //TODO support more types
    }

    public class UIMaterialPropertyUpdater : SerializedMonoBehaviour
    {
        [ShowIf("PropertyType", MaterialPropertyType.COLOR)]
        public IValue<Color> ColorValue;

        public bool DebugLog;

        [ShowIf("PropertyType", MaterialPropertyType.FLOAT)] [FormerlySerializedAs("Value")]
        public IValue<float> FloatValue;

        private int propertyID;

        [ValueDropdown("PropertyIds")] public string PropertyName;

        public MaterialPropertyType PropertyType;

        public bool SetOnEnable = true;

        [FormerlySerializedAs("SetPropertyEveryFrame")]
        public bool SetPropertyRegularly;

        //note that this class does not use material property blocks to be usable in the canvas renderer

        public CanvasRenderer Target;

        [ShowIf("PropertyType", MaterialPropertyType.TEXTURE)] [FormerlySerializedAs("Value")]
        public IValue<Texture> TextureValue;

        public float UpdateFrequency;

        private WaitForSeconds waitingCoroutine;

#if UNITY_EDITOR
        private ValueDropdownList<string> PropertyIds =>
            ShaderUtilities.ShaderPropertyNames(Target.GetMaterial(), PropertyType);
#endif

        public void Awake()
        {
            propertyID = Shader.PropertyToID(PropertyName);
        }

        public void OnEnable()
        {
            if (SetOnEnable)
                UpdateMaterial();
            if (SetPropertyRegularly)
                StartToUpdateEveryFrame();
        }

        public void UpdateMaterial()
        {
            UpdateMaterial(Target.GetMaterial());
        }

        protected virtual void UpdateMaterial(Material mat)
        {
            if (!mat)
                return;

            switch (PropertyType)
            {
                case MaterialPropertyType.FLOAT:
                    mat.SetFloat(propertyID, FloatValue.Value);
#if UNITY_EDITOR
                    if (DebugLog)
                        UnityEngine.Debug.Log($"Set Float Property to value {FloatValue.Value}");
#endif
                    break;
                case MaterialPropertyType.COLOR:
                    mat.SetColor(propertyID, ColorValue.Value);
                    break;
                case MaterialPropertyType.TEXTURE:
                    mat.SetTexture(propertyID, TextureValue.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void StartToUpdateEveryFrame()
        {
            waitingCoroutine = new WaitForSeconds(UpdateFrequency);
            StartCoroutine(UpdateOverTime());
        }

        public void StopToUpdateEveryFramne()
        {
            StopAllCoroutines();
        }

        private IEnumerator UpdateOverTime()
        {
            while (true)
            {
                UpdateMaterial();
                yield return waitingCoroutine;
            }
        }
    }
}
