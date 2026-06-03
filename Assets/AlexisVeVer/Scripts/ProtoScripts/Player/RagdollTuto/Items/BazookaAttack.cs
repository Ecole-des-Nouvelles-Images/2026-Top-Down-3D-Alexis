using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class BazookaAttack : Weapon
    {
        [SerializeField] private GameObject _rocketPrefab;
        [SerializeField] private GameObject _prePlacedRocket;
        [SerializeField] private float _rocketLaunchingForce;
        
        private Rigidbody _rocketRb;

        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            GameObject rocket = Instantiate(_rocketPrefab, _prePlacedRocket.transform.position, _prePlacedRocket.transform.rotation);
            Destroy(_prePlacedRocket);
            _rocketRb = rocket.GetComponent<Rigidbody>();
            _rocketRb.AddForce(new Vector3(-CurrentHolder.moveInput.x, 0, CurrentHolder.moveInput.y) * _rocketLaunchingForce, 
                ForceMode.Impulse);
            Destroy(gameObject);
        }

        public override void AutoUse(PlayerController playerController) { }
    }
}
