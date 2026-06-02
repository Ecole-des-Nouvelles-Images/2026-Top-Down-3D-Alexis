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

        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _drawScreen;
        
        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
            if (players.Count == 1)
            {
                Time.timeScale = 0;
                _victoryScreen.SetActive(true);
            }

            if (players.Count < 1)
            {
                Time.timeScale = 0;
                _drawScreen.SetActive(true);
            }
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

        public void PlayerDead(GameObject deadPlayer)
        {
            foreach (GameObject player in players)
            {
                if (player == deadPlayer)
                {
                    players.Remove(player);
                    playerHealths.Remove(player.GetComponent<PlayerHealth>());
                }
            }
        }
    }
}