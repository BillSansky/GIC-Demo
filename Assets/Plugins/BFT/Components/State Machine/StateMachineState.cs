using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    public class StateMachineState : MonoBehaviour
    {
        /* [BoxGroup("Transitions")]
     public IGenericValue<bool> EnterFromAnyStateCondition;
     [BoxGroup("Transitions")]
     public Dictionary<IGenericValue<bool>, StateMachineState> StateTransitions;*/

        [BoxGroup("Tools")] public bool DebugLog;

        [BoxGroup("Events")] public UnityEvent OnStateEnter;
        [BoxGroup("Events")] public UnityEvent OnStateExit;


        public StateMachineState Data
        {
            get => this;
            set => UnityEngine.Debug.LogWarning("Cannot set the state of a state", this);
        }

        public string NameID
        {
            get => name;
            set => name = value;
        }

        public void NotifyStateEnter()
        {
            if (DebugLog)
                UnityEngine.Debug.Log($"State {name} entered", this);
            gameObject.SetActive(true);
            OnStateEnter.Invoke();
        }

        public void NotifyStateExit()
        {
            if (DebugLog)
                UnityEngine.Debug.Log($"State {name} exited", this);
            OnStateExit.Invoke();
            gameObject.SetActive(false);
        }


        public JsonData ExportJsonData()
        {
            throw new System.NotImplementedException();
        }

        public void ParseJsonData(JsonData data)
        {
            throw new System.NotImplementedException();
        }

        public void NotifyJSonDataDeleteRequest()
        {
            throw new System.NotImplementedException();
        }
    }
}
