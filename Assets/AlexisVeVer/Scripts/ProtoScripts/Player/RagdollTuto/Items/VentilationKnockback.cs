using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class VentilationKnockback : MonoBehaviour
    {
        [SerializeField] private float _ventilationForce;
        
        private Rigidbody _rb;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _rb = other.GetComponent<Rigidbody>();
            }
        }

        public void VentilationPushesPlayers(PlayerController playerController)
        {
            if (_rb == null) return;
            _rb.AddForce(new Vector3(playerController.moveInput.x, 0, playerController.moveInput.y) * _ventilationForce, ForceMode.Impulse);
        }
    }
}
