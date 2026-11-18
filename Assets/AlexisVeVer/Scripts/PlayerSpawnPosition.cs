using System;
using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts
{
    public class PlayerSpawnPosition : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPoints;
        private int _spawnPointIndex;
        
        public void OnPlayerJoined(PlayerInput player)
        {
            print("replace player");
            Transform playerController = player.GetComponent<Transform>();
            playerController.transform.position = spawnPoints[_spawnPointIndex].position;
            _spawnPointIndex++; // the position
            if (_spawnPointIndex >= spawnPoints.Count)
            {
                _spawnPointIndex = 0;
            }
            
        }

        private void OnDrawGizmosSelected() {
            foreach (var point in spawnPoints) {
                if (point == null) continue;
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(point.position, 0.4f);
            }
        }
    }
    
}
