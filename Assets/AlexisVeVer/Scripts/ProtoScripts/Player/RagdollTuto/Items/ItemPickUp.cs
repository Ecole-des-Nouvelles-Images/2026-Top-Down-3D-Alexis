using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotationVector;
        [SerializeField] private float _timeBeforeDestroy;

        private PlayerController _playerController;
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        void Update()
        {
            ItemRotation();
        }
        
        private void ItemRotation()
        {
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles + _rotationVector * Time.deltaTime);
          
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerController = other.GetComponent<PlayerController>();
                
                if (_playerController.WeaponEquipped) return;
                _playerController.Equip(_weapon);
            }
        }
    }
}