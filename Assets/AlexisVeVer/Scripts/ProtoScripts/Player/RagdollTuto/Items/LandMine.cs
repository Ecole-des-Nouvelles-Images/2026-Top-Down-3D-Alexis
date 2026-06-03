using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class LandMine : Weapon
    {
        
        
        [SerializeField] private float _mineThrowingForce;
        [SerializeField] private float _gravityForce;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Quaternion _groundedMineRotation;
        [SerializeField] private GameObject _explosionRadius;
        
        
        
        
        private Rigidbody _rb;
        
        // bool that lets the landmine stick to the ground after it is sent
        [SerializeField] private bool _mineSent;
        
        // Allows the LandMine to blow
        private bool _mineCanBlow;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            // Retirer parentage
            gameObject.transform.SetParent(null);
            
            // Déséquipper la mine
            playerController.UnEquip();
            
            // Lui donner de la force
            _rb.AddForce(playerController.moveInput.x * _mineThrowingForce, -_gravityForce, playerController.moveInput.y * _mineThrowingForce);
            
            // La parenter au sol quand elle touche le sol
            _mineSent = true;
            
            Debug.Log("MineSent");
        }

        public override void AutoUse(PlayerController playerController) { }


        private void MineBlowsUp()
        {
            // Instancier le vfx d'explosion + déclencher le rayon d'explosion
            _explosionRadius.SetActive(true);
            
            Destroy(gameObject, 0.2f);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground") && _mineSent)
            {
                _rb.linearVelocity = Vector3.zero;
                gameObject.transform.SetParent(other.transform);
                gameObject.transform.rotation = _groundedMineRotation;
                _mineCanBlow = true;
                _mineSent = false;
            }

            if (other.gameObject.CompareTag("Player") && _mineCanBlow)
            {
                Debug.Log("Mine explosion");
                MineBlowsUp();
            }
        }
    }
}