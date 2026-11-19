using System;
using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using Player;
using UI;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace AlexisVeVer.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public List<PlayerController> players = new List<PlayerController>();
        public List<PlayerHealth> playerHealths = new List<PlayerHealth>();
        
        [SerializeField] private PlayerPanel[] _playerPanels;
        [SerializeField] private GameObject _victoryScreen;
        [SerializeField] private GameObject _drawScreen;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private CinemachineTargetGroup _targetGroup;

        private PlayerSpawnPosition _playerSpawnPosition;

        private bool _timeInGame;
        
        void Awake()
        {
            _playerSpawnPosition = gameObject.GetComponent<PlayerSpawnPosition>();
            if (Instance == null) Instance = this;
        }

        void Start()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
            // if (players.Count == 1)
            // {
            //     _victoryScreen.SetActive(true);
            // }
            //
            // if (players.Count < 1)
            // {
            //     _drawScreen.SetActive(true);
            // }
           
            if (Time.timeScale >= 1)
            {
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
            }
        }
        
        public void AddPlayer(PlayerController player)
        {
            player.OnPausePressed += PlayerOnPausePressed;
            players.Add(player);
            playerHealths.Add(player.GetComponent<PlayerHealth>());
            playerHealths[playerHealths.Count - 1].OnDeath += OnDeath;
            _playerPanels[players.Count - 1].gameObject.SetActive(true);
            _playerPanels[players.Count - 1].SetupUi(player);
            _targetGroup.AddMember(player.transform, 1, 1);
        }

        private void PlayerOnPausePressed(object sender, EventArgs e)
        {
            if (_pauseMenu.activeSelf) {
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
            }
            else {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
            }
        }

        private void OnDeath(object sender, PlayerController playerController) {
            
            foreach (PlayerController player in players) {
                
                if (player == playerController) {
                    players.Remove(playerController);
                    return;
                }
            }
            playerHealths.Remove((PlayerHealth)sender);
            
            if (players.Count == 1)
            {
                _victoryScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }

        public void RemovePlayer(PlayerController player)
        {
            players.Remove(player);
            playerHealths.Remove(player.GetComponent<PlayerHealth>());
        }

        public void PlayerDead(GameObject deadPlayer)
        {
            for(int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i] == deadPlayer)
                {
                    players.RemoveAt(i);
                }
            }
        }
    }
}