using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public List<GameObject> players = new List<GameObject>();
        public List<PlayerHealth> playerHealths = new List<PlayerHealth>();
        
        [SerializeField] private List<PlayerHealth> _assignedPlayerHealths = new List<PlayerHealth>();
        private int _playerHealthIndex;
        
        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void AddPlayer(GameObject player)
        {
            players.Add(player);
            playerHealths.Add(player.GetComponent<PlayerHealth>());
        }

        public void RemovePlayer(GameObject player)
        {
            players.Remove(player);
            playerHealths.Remove(player.GetComponent<PlayerHealth>());
        }

        public PlayerHealth GetHealthComponent()
        {
            if (playerHealths[0] == null)
            {
                Debug.Log("No playerHealthFound");
                return null;
            }

            if (playerHealths.Count > 0)
            {
                _assignedPlayerHealths.Add(playerHealths[0]);
                playerHealths.RemoveAt(0);
                return _assignedPlayerHealths[_assignedPlayerHealths.Count - 1];
            }
            
            // foreach (PlayerHealth playerHealth in playerHealths)
            // {
            //     if (!_assignedPlayerHealths.Contains(playerHealth))
            //     {
            //         _assignedPlayerHealths.Add(playerHealth);
            //         playerHealths.Remove(playerHealth);
            //         Debug.Log("Assigned PlayerHealth : " + playerHealth);
            //         return playerHealth;
            //     }
            // }
            //
            // if (playerHealths.Count == 1)
            // {
            //     return playerHealths[0];
            // }
            
            Debug.Log("No PlayerHealth assigned, all are already assigned.");
            return null;
        }
    }
}
