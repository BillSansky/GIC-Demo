using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     Contains a list of action to execute
    /// </summary>
    public class ActionListComponent : ListComponent<Action>
    {
        [SerializeField, BoxGroup("Options")] private bool ExecuteOnEnable = false;

        [Button(ButtonSizes.Medium), BoxGroup("Tools")]
        public void Execute()
        {
            foreach (var func in this)
            {
                func.Invoke();
            }
        }

        public void OnEnable()
        {
            if (ExecuteOnEnable)
                StartCoroutine(AutoExecuteActions());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator AutoExecuteActions()
        {
            while (ExecuteOnEnable)
            {
                Execute();
                yield return null;
            }
        }
    }
}
