using Player;
using UnityEngine;

namespace Items
{
    public class Hammer : Weapon
    {
        [Header("AttackReferences")]
        [SerializeField] private GameObject _hitbox;
        [SerializeField] private ItemAttack _itemAttack;
        
        [Header("DamageReferences"), Space(10)]
        [SerializeField] private float _hitDamage;
        [SerializeField] private float _hitStun;
        [SerializeField] private float _hitKnockBack;
        
        [Header("VfxReferences"), Space(10)]
        [SerializeField] private GameObject _attackVfx;
        [SerializeField] private GameObject _decal;
        [SerializeField] private GameObject _hitParticle;
        [SerializeField] private Vector3 _vfxOffset;
        
        [Header("other"), Space(10)]
        [SerializeField] private GameObject _itemPickUp;

        private bool _hitboxSet;
        private LayerMask _playerLayerMask;
        
        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            playerController.characterAnimator.SetTrigger("HammerAttack");
        }

        public override void AutoUse(PlayerController playerController) { }

        private void Awake()
        {
            _itemAttack.onHit += OnHit;
        }

        private void Update()
        {
            if (!_itemPickUp.activeSelf && !_hitboxSet)
            {
                _playerLayerMask = LayerMask.NameToLayer(CurrentHolder.gameObject.layer.ToString());
                _hitbox.transform.SetParent(GetComponentInParent<PlayerController>().transform);
                _hitbox.transform.localPosition = Vector3.zero;
                _hitbox.transform.localRotation = Quaternion.Euler(0, 0, 0);
                transform.localRotation = Quaternion.Euler(-90, 0, -90);
                _hitboxSet = true;
            }
        }
        
        public void OnAttack()
        {
            _hitbox.SetActive(true);
            Instantiate(_attackVfx, _hitbox.transform.position - _vfxOffset, Quaternion.Euler(-90, 0, 0));
            Instantiate(_decal, _hitbox.transform.position  - _vfxOffset, Quaternion.Euler(90, 0, 0));
        }
        
        private void OnHit(object itemDamage, Collider collider)
        {
            if (collider == null || collider.GetComponent<PlayerController>() != CurrentHolder) return;
            collider.GetComponent<PlayerHealth>().GetHit(_hitDamage,  _hitStun, _hitKnockBack, gameObject);
            Instantiate(_hitParticle, collider.transform.position, Quaternion.identity);
        }

        public void DestroyHammer()
        {
            CurrentHolder.UnEquip();
            Destroy(_hitbox);
            Destroy(gameObject);
        }
    }
}