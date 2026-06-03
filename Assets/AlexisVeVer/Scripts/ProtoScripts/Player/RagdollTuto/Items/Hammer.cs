using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class Hammer : Weapon
    {
        [SerializeField] private GameObject _hitbox;
        
        [SerializeField] private GameObject _attackVfx;
        [SerializeField] private GameObject _decal;
        [SerializeField] private float _animationTime;
        
        public override void Equip(PlayerController playerController)
        {
            CurrentHolder = playerController;
        }

        public override void Use(PlayerController playerController)
        {
            playerController.characterAnimator.SetTrigger("HammerAttack");
        }

        public override void AutoUse(PlayerController playerController) { }

        public void OnAttack()
        {
            _hitbox.SetActive(true);
            Instantiate(_attackVfx, new Vector3(_hitbox.transform.position.x, -0.1f, _hitbox.transform.position.z), Quaternion.identity);
            Instantiate(_decal, new Vector3(_hitbox.transform.position.x, -0.5f, _hitbox.transform.position.z), Quaternion.Euler(-90, 0, 0));
            
            Invoke(nameof(CurrentHolder.UnEquip), _animationTime);
            Destroy(gameObject, _animationTime);
        }

        public void EndAttack()
        {
            _hitbox.SetActive(false);
        }
    }
}