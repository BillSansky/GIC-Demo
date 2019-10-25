using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BFT
{
    public class PropertyComponent : MonoBehaviour
    {
        [FormerlySerializedAs("Category")] [InlineEditor(InlineEditorModes.GUIAndHeader), OnValueChanged("CheckType")]
        public PropertyType PropertyType;

        [OnValueChanged("CheckType")] public virtual object Data { get; }

        protected virtual void CheckType()
        {
            if (!PropertyType.ExpectsDataType)
                return;
            var type = GetType();
            if (type != PropertyType.DataTypeExpected)
            {
                PropertyType = null;
                Debug.LogWarning(
                    $"The property type expects a property of type {PropertyType.DataTypeExpected} but the current property is of type {type}",
                    this);
            }
        }
    }
}
