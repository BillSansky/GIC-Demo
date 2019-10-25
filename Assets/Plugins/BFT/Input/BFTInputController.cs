using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if BFT_REWIRED
using Rewired;

namespace BFT
{
    public class BFTInputController : SerializedMonoBehaviour
    {
        public Dictionary<Player, List<InputHandler>> HandlersByPlayer
            = new Dictionary<Player, List<InputHandler>>();

        public List<string> PlayerNames;

        [HideInInspector] public List<Player> Players = new List<Player>();

        public void Awake()
        {
            if (ReInput.players == null)
                return;
            foreach (var playerName in PlayerNames)
            {
                Player player = ReInput.players.GetPlayer(playerName);

                if (player == null)
                {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogWarning("The player name " + "'" + playerName + "'" + " wasn't recognized!");
#endif
                    continue;
                }

                Players.Add(player);

                foreach (var handlers in HandlersByPlayer[player])
                {
                    handlers.AssignPlayer(player);
                }
            }
        }
    }
}

#endif
