using Items;
using Player;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class LandMine : Weapon
    {
        [SerializeField] private float _mineThrowingForce;
        [SerializeField] private float _gravityForce;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Quaternion _groundedMineRotation;
        [SerializeField] private GameObject _explosionRadius;
        
        private Rigidbody _rb;
        
        // bool that lets the landmine stick to the ground after it is sent
        private bool _mineSent;
        
        // Allows the LandMine to blow
        private bool _mineCanBlow;

        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            _rb = GetComponent<Rigidbody>();
            
            // Retirer parentage
            gameObject.transform.SetParent(null);
            
            // Lui donner de la force
            _rb.AddForce((playerController.transform.forward * _mineThrowingForce) + new Vector3(0,- _gravityForce, 0), ForceMode.Impulse);
            
            // Autoriser le parentage au sol
            _mineSent = true;
            
            // Déséquipper la mine
            playerController.UnEquip();
        }

        public override void AutoUse(PlayerController playerController) { }

        private void Update()
        {
            bool rbChecked = false;
            bool layerChanged = false;
            
            if (GetComponent<Rigidbody>() != null && rbChecked == false)
            {
                _rb = GetComponent<Rigidbody>();
                rbChecked = true;
            }

            if (_rb != null && layerChanged == false)
            { 
                _rb.excludeLayers = _playerLayer;
                layerChanged = true;
            }
        }
        
        
        private void MineBlowsUp()
        {
            // Instancier le vfx d'explosion + déclencher le rayon d'explosion
            Instantiate(_explosionRadius, gameObject.transform.position, gameObject.transform.rotation);
            Debug.Log("Explosion radius active");
            
            Destroy(gameObject, 0.2f);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_layerMask.value == other.gameObject.layer && _mineSent)
            {
                Debug.Log("MineSent " + _mineSent);
                _rb.useGravity = false;
                _rb.linearVelocity = Vector3.zero;
                gameObject.transform.SetParent(other.transform);
                gameObject.transform.rotation = _groundedMineRotation;
                _mineCanBlow = true;
                _mineSent = false;
            }

            if (other.gameObject.CompareTag("Player") && _mineCanBlow)
            {
                MineBlowsUp();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == _layerMask && _mineSent) Debug.Log("MineSent " + _mineSent);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == _layerMask && _mineSent) Debug.Log("MineSent " + _mineSent);
        }
    }
}