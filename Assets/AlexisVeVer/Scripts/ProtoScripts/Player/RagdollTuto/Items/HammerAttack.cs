using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class HammerAttack : Weapon
    {
        [SerializeField] private GameObject _hitbox;
        
        [SerializeField] private GameObject _attackVfx;
        [SerializeField] private GameObject _decal;
        [SerializeField] private float _animationTime;
        
        private PlayerController _playerController;
        
        public override void Equip(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            playerController.CharacterAnimator.SetTrigger("HammerAttack");
        }

        public override void AutoUse(PlayerController playerController) {}

        public void OnAttack()
        {
            _hitbox.SetActive(true);
            Instantiate(_attackVfx, _hitbox.transform.position, Quaternion.identity);
            Instantiate(_decal, _hitbox.transform.position, Quaternion.identity);
            
            Invoke(nameof(_playerController.UnEquip), _animationTime);
            Destroy(gameObject, _animationTime);
        }
    }
}