using Player;
using UnityEngine;

namespace Items
{
    public class Fan : Weapon
    {
        [SerializeField] private GameObject _ventilation;
        [SerializeField] private float _fanDuration;
        [SerializeField] private float _userPushBackForce;
        
        [Header("other"), Space(10)]
        [SerializeField] private GameObject _itemPickUp;
        
        private Rigidbody _rb;
        private bool _isVentilationActive;
        private bool _hitboxSet;
        
        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
            //_rb = playerController.gameObject.GetComponent<Rigidbody>();
            Invoke(nameof(DestroyMe), _fanDuration);
        }

        public override void Use(PlayerController playerController)
        {
            
        }

        public override void AutoUse(PlayerController playerController)
        {
            CurrentHolder = playerController;
            // Avoir un collider devant qui donne une force à ce qu'il touche
            if (!_isVentilationActive)
            {
                _ventilation.SetActive(true);
                _isVentilationActive = true;
            }
            
            // Repousser en arrière le joueur qui le tiens
            playerController.rb.AddForce(- playerController.transform.forward * (_userPushBackForce * Time.deltaTime), ForceMode.Impulse);
        }
        
        private void Update()
        {
            if (!_itemPickUp.activeSelf && !_hitboxSet)
            {
                _ventilation.transform.SetParent(GetComponentInParent<PlayerController>().transform);
                _ventilation.transform.localPosition = new Vector3(0, 1, 0);
                _ventilation.transform.localRotation = Quaternion.Euler(90, 0, 0);
                //transform.localRotation = Quaternion.Euler(-90, 0, -90);
                _hitboxSet = true;
            }

            if (_hitboxSet)
            {
                transform.localPosition = Vector3.zero;
            }
        }

        private void DestroyMe()
        {
            CurrentHolder.UnEquip();
            Destroy(_ventilation);
            Destroy(gameObject);
        }
    }
}
