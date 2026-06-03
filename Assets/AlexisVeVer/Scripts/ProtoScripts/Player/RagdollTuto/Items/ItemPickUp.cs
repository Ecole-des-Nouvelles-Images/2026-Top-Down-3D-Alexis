using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class ItemPickUp : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Vector3 _rotationVector;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _timeBeforeDestroy;
        [SerializeField] private float _radius;
        
        private PlayerController _playerController;
        private Weapon _weapon;

        private void Awake()
        {
            _weapon = GetComponentInParent<Weapon>();
        }

        void Update()
        {
            ItemRotation();
            RaycastHit[] hits =Physics.SphereCastAll(
                _parent.transform.position + _offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                _parent.transform.SetParent(null);
                return;
            }
            
            _parent.transform.SetParent(hits[0].transform);
        }
        
        private void ItemRotation()
        {
            _parent.transform.rotation = Quaternion.Euler(_parent.transform.rotation.eulerAngles + _rotationVector * Time.deltaTime);
          
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _playerController = other.GetComponent<PlayerController>();
                
                if (_playerController.WeaponEquipped) return;
                _playerController.Equip(_weapon);
                gameObject.SetActive(false);
            }
        }
    }
}