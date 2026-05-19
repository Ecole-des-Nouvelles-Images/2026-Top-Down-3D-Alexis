using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GrabZoneDetection : MonoBehaviour
    {
        private PlayerController _playerController;
        
        public List<Collider> EnnemyPlayerColliders;

        private void Start()
        {
            _playerController = GetComponentInParent<PlayerController>();
            EnnemyPlayerColliders = new List<Collider>();
        }

        private void Update()
        {
            if (EnnemyPlayerColliders.Count > 0)
            {
                _playerController.PlayerInGrabZone = true;
            }
            else
            {
                _playerController.PlayerInGrabZone = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("A collidé un truc");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Un joueur est dans la zone de grab");
                EnnemyPlayerColliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            int i = 0;
            foreach (Collider ennemyColliders in EnnemyPlayerColliders)
            {
                if (other == EnnemyPlayerColliders[i])
                {
                    EnnemyPlayerColliders.Remove(other);
                }
                i++;
            }
        }
    }
}