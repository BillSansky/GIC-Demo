#if BFT_REWIRED

using System;
using System.Collections;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

using System.Collections.Generic;

namespace BFT
{
    public class InputHandler : MonoBehaviour, IValue<bool>
    {
        [BoxGroup("Player")] public bool AutoAssignPlayerOnEnable;

        [BoxGroup("Input")] public List<AxisActionPair> AxisByNames = new List<AxisActionPair>();

        [BoxGroup("Input")] public List<ButtonActionPair> ButtonsByNames = new List<ButtonActionPair>();

        [BoxGroup("Tools")] public bool DebugLog;

        [ShowIf("AutoAssignPlayerOnEnable")] [BoxGroup("Player")]
        public string PlayerNameToAssign;


        [SerializeField, ReadOnly] [BoxGroup("Status")]
        private Player playerToHandle;

        public int PlayerToHandleID => playerToHandle.id;

        public bool Value => playerToHandle.GetAnyButton();

        public void OnEnable()
        {
            StartCoroutine(WaitUntilRewiredReady());
        }

        public IEnumerator WaitUntilRewiredReady()
        {
            while (!ReInput.isReady)
                yield return null;

            if (AutoAssignPlayerOnEnable)
            {
                playerToHandle = ReInput.players.GetPlayer(PlayerNameToAssign);

                if (DebugLog)
                {
                    if (playerToHandle == null)
                        UnityEngine.Debug.LogWarningFormat(this,
                            "The player named {0} could not be found by Input Handler {1}",
                            PlayerNameToAssign, name);
                }
            }
        }

        void Update()
        {
            playerToHandle.IfNotNull(HandleInputPerPlayer);
        }

        public void AssignPlayer(string playerName)
        {
            playerToHandle = ReInput.players.GetPlayer(playerName);
        }

        public void AssignPlayer(Player player)
        {
            playerToHandle = player;
        }

        protected virtual void HandleInputPerPlayer(Player player)
        {
            if (AxisByNames != null)
            {
                foreach (var pair in AxisByNames)
                {
                    if (pair.action)
                        pair.action.CheckAxis(player, pair.id);
                }
            }

            if (ButtonsByNames != null)
            {
                foreach (var pair in ButtonsByNames)
                {
                    if (pair.action)
                        pair.action.CheckButton(player, pair.id);
                }
            }
        }

        [Serializable]
        public class AxisActionPair
        {
            public AxisAction action;
            [ValueDropdown("Ids")] public int id;

            private ValueDropdownList<int> Ids
            {
                get
                {
                    ValueDropdownList<int> dd = new ValueDropdownList<int>();
                    InputManager manager = FindObjectOfType<InputManager>();

                    manager.runInEditMode = true;

                    try
                    {
                        foreach (var action in ReInput.mapping.Actions)
                        {
                            dd.Add(action.name, action.id);
                        }
                    }
                    catch (Exception)
                    {
                        return dd;
                    }

                    manager.runInEditMode = false;
                    UnityEngine.Debug.ClearDeveloperConsole();

                    return dd;
                }
            }
        }

        [Serializable]
        public class ButtonActionPair
        {
            public ButtonAction action;
            [ValueDropdown("Ids")] public int id;

            private ValueDropdownList<int> Ids
            {
                get
                {
                    ValueDropdownList<int> dd = new ValueDropdownList<int>();
                    InputManager manager = FindObjectOfType<InputManager>();

                    manager.runInEditMode = true;
                    try
                    {
                        foreach (var action in ReInput.mapping.Actions)
                        {
                            dd.Add(action.name, action.id);
                        }
                    }
                    catch (Exception)
                    {
                        return dd;
                    }

                    manager.runInEditMode = false;
                    UnityEngine.Debug.ClearDeveloperConsole();
                    return dd;
                }
            }
        }
    }
}

#endif
