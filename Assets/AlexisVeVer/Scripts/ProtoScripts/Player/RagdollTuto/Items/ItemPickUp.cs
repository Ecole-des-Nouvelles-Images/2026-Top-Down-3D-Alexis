using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector3 _rotationVector;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _timeBeforeDestroy;
        [SerializeField] private float _radius;
        
        private PlayerController _playerController;
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        void Update()
        {
            ItemRotation();
            RaycastHit[] hits =Physics.SphereCastAll(
                transform.position + _offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                gameObject.transform.SetParent(null);
                return;
            }
            
            gameObject.transform.SetParent(hits[0].transform);
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