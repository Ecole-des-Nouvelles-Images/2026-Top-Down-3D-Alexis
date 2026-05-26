using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class FanPropelsAir : Weapon
    {
        [SerializeField] private GameObject _ventilation;
        [SerializeField] private float _userPushBackForce;
        
        private PlayerController _playerController;
        private Rigidbody _rb;
        private bool _isVentilationActive;
        
        public override void Equip(PlayerController playerController)
        {
            _rb = playerController.GetComponent<Rigidbody>();
            playerController.UnEquip();
            Invoke(nameof(DestroyMe), 3f);
        }

        public override void Use(PlayerController playerController) { }

        public override void AutoUse(PlayerController playerController)
        {
            _playerController = playerController;
            Debug.Log("FanVentilates");
            // Avoir un collider devant qui donne une force à ce qu'il touche
            if (!_isVentilationActive)
            {
                _ventilation.SetActive(true);
                _isVentilationActive = true;
            }
            
            // Repousser en arrière le joueur qui le tiens
            _rb.AddRelativeForce(-playerController.moveInput * _userPushBackForce);
        }

        private void Update()
        {
            if (_isVentilationActive)
            {
                _ventilation.GetComponent<VentilationKnockback>().VentilationPushesPlayers(_playerController);
            }
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
