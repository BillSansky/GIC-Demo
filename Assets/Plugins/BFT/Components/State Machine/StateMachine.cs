using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    public class StateMachine : MonoBehaviour
    {
        [BoxGroup("Status"), ReadOnly] public StateMachineState CurrentState;
        public bool InitStateOnEnable;

        [BoxGroup("Debug")] public bool LogDebug;

        [BoxGroup("States"), Required] public StateMachineState StartingState;

        [BoxGroup("States")]
        public Dictionary<int, StateMachineState> StateMachineStates = new Dictionary<int, StateMachineState>();

        public List<StateMachineState> States = new List<StateMachineState>();

        [BoxGroup("Tools"), Button(ButtonSizes.Medium)]
        private void AddState()
        {
            GameObject newGo = new GameObject("New State");
            newGo.transform.SetParent(transform);

            StateMachineState state = newGo.AddComponent<StateMachineState>();
            States.Add(state);
            if (!StartingState)
                StartingState = state;
        }

        public void Awake()
        {
            InitState();
        }

        public void OnEnable()
        {
            if (InitStateOnEnable)
                InitState();
        }

        public void InitState()
        {
            for (var i = 0; i < States.Count; i++)
            {
                var machineState = States[i];
                StateMachineStates.Add(i, machineState);
            }


            foreach (var state in StateMachineStates)
            {
                state.Value.gameObject.SetActive(false);
            }

            TransitionToState(StartingState);
        }

        /* public void Update()
     {
         if (CurrentState.StateTransitions != null)
             foreach (var transition in CurrentState.StateTransitions)
             {
                 if (transition.Key == null || transition.Key.Value)
                 {
                     ForceTransition(transition.Value);
                     return;
                 }
             }
 
         foreach (var state in StateMachineStates)
         {
             if (state.Value.EnterFromAnyStateCondition != null && state.Value.EnterFromAnyStateCondition.Value)
             {
                 ForceTransition(state.Value);
                 return;
             }
         }
     }*/

        public void TransitionToState(int id)
        {
            TransitionToState(!StateMachineStates.ContainsKey(id) ? StartingState : StateMachineStates[id]);
        }


        public void TransitionToState(UnityEngine.Object newState)
        {
            if (newState is GameObject go)
                newState = go.GetComponent<StateMachineState>();

            TransitionToState((StateMachineState) newState);
        }

        public void TransitionToState(StateMachineState newState)
        {
            UnityEngine.Debug.Assert(StateMachineStates.ContainsValue(newState),
                "The state is not present in the state machine, this surely will cause issues", this);

            if (newState == null)
                newState = StartingState;

            if (LogDebug)
                UnityEngine.Debug.LogFormat(this, "Transition from state {0} to state {1}", CurrentState ? CurrentState.name : "None",
                    newState.name);

            if (CurrentState)
                CurrentState.NotifyStateExit();
            newState.NotifyStateEnter();
            CurrentState = newState;
        }

        public bool IsCurrentState(StateMachineState state)
        {
            return CurrentState == state;
        }

        public bool IsObjectCurrentState(UnityEngine.Object state)
        {
            return CurrentState == state || CurrentState.gameObject == state;
        }
    }
}
