using Player;
using UnityEngine;

namespace Items
{
    public class Katana : Weapon
    {
        
        [Header("HitboxReferences"),  Space(10)]
        [SerializeField] private GameObject _hitbox;
        [SerializeField] private ItemAttack _itemAttack;
        
        [Header("DamageReferences"), Space(10)]
        [SerializeField] private float _hitDamage;
        [SerializeField] private float _hitStun;
        [SerializeField] private float _hitKnockBack;
        
        [Header("VfxReferences"), Space(10)]
        [SerializeField] private GameObject _hitParticle;
        
        private void Awake()
        {
            _itemAttack.onHit += OnHit;
        }

        private void OnHit(object itemDamage, Collider collider)
        {
            if (collider == null || collider.GetComponentInParent<PlayerController>() == CurrentHolder) return;
            collider.GetComponent<PlayerHealth>().GetHit(_hitDamage,  _hitStun, _hitKnockBack, gameObject);
            Instantiate(_hitParticle, collider.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }
        
        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            _hitbox.SetActive(true);
            playerController.characterAnimator.SetTrigger("KatanaAttack");
        }

        public override void AutoUse(PlayerController playerController) { }
    }
}
