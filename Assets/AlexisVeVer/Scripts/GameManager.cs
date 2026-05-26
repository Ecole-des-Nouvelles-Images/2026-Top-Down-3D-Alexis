using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public List<GameObject> players = new List<GameObject>();
        public List<PlayerHealth> playerHealths = new List<PlayerHealth>();
        
        private List<PlayerHealth> _assignedPlayerHealths = new List<PlayerHealth>();
        private int _playerHealthIndex;
        
        void Awake()
        {
            if (instance == null) instance = this;
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
            if (playerHealths.Count == 0) return null;
            foreach (PlayerHealth playerHealth in playerHealths)
            {
                if (!_assignedPlayerHealths.Contains(playerHealth))
                {
                    _assignedPlayerHealths.Add(playerHealth);
                    return playerHealth;
                }
            }
            return null;
        }
    }
}
