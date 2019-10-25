using System.Collections;

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
#if BFT_TEXTMESHPRO
using BFT;
using TMPro;

namespace Plugins.BFT.UI.Text
{
    public class TextUpdater : MonoBehaviour, IVariable<string>
    {
        [BoxGroup("Options")] public bool AllowUpdateWhenInactive = true;

        [BoxGroup("Options")] public bool AutoUpdate;

        [BoxGroup("Events")] public bool EmitEvent;

        [BoxGroup("Events")] [ShowIf("EmitEvent")]
        public UnityEvent OnTextUpdated;

        [BoxGroup("Referencing")] public StringValue StringReference = new StringValue() {UseReference = true};

        [SerializeField] private string style;
        [BoxGroup("Referencing")] public TMP_Text Text;

        [BoxGroup("Options")] public bool UpdateOnAwake = false;

        [BoxGroup("Options"), HideIf("UpdateOnStartThenOnEnable")]
        public bool UpdateOnEnable = true;

        [BoxGroup("Options"), HideIf("UpdateOnStartThenOnEnable")]
        public bool UpdateOnStart = false;

        [OnValueChanged("SetEnableUpdateToFalse")] [BoxGroup("Options")]
        public bool UpdateOnStartThenOnEnable = false;

        public string Value
        {
            get => StringReference.Value;

            set
            {
                var variable = StringReference as IVariable<string>;
                if (variable == null)
                {
                    UnityEngine.Debug.LogWarning("Tried to set the value of a string value that is not a variable: this is not allowed",
                        this);
                    return;
                }

                variable.Value = value;
                UpdateText();
            }
        }

        private void SetEnableUpdateToFalse()
        {
            if (UpdateOnStartThenOnEnable)
            {
                UpdateOnEnable = false;
                UpdateOnStart = false;
            }
        }

        public void SetAutoUpdate(bool value)
        {
            AutoUpdate = value;

            if (AutoUpdate)
            {
                StartCoroutine(CheckText());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        void Awake()
        {
            if (UpdateOnAwake)
            {
                UpdateText();
            }
        }

        void Start()
        {
            if (UpdateOnStart)
            {
                UpdateText();
            }
            else if (UpdateOnStartThenOnEnable)
            {
                UpdateText();
                UpdateOnEnable = true;
            }
        }

        void OnEnable()
        {
            if (UpdateOnEnable)
            {
                UpdateText();
            }

            if (AutoUpdate)
            {
                StartCoroutine(CheckText());
            }
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator CheckText()
        {
            while (true)
            {
                UpdateText();
                yield return null;
            }
        }

        [BoxGroup("Tools"), Button(ButtonSizes.Medium)]
        public void UpdateText()
        {
            if (!AllowUpdateWhenInactive && !gameObject.activeInHierarchy || !enabled)
                return;

            UpdateText(StringReference.Value);
        }

        public void UpdateText(string text)
        {
            if (!AllowUpdateWhenInactive && !gameObject.activeInHierarchy || !enabled)
                return;

            Text.text = style.IsNullOrEmpty() ? text : ("<style=" + style + ">" + text);
            if (EmitEvent)
            {
                OnTextUpdated.Invoke();
            }
        }

#if UNITY_EDITOR

        private void CheckIfSelf()
        {
            if (Equals(StringReference, this))
            {
                StringReference = null;
                UnityEngine.Debug.LogWarning("A Text Updater cannot reference itself (would cause a stack overflow)", this);
            }
        }

        void Reset()
        {
            Text = GetComponent<TMP_Text>();
        }

        public void OnDrawGizmos()
        {
            if (!Application.isPlaying && AutoUpdate && StringReference != null && Text != null &&
                !StringReference.Value.IsNullOrEmpty())
            {
                UpdateText();
            }
        }

#endif
    }
}
#endif