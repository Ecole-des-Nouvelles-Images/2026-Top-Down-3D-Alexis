using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player
{
    public class AnimationEvents : MonoBehaviour
    {
        [Header("PlayerController")]
        [SerializeField] private PlayerController _playerController;
        
        [Header("Sounds")]
        [SerializeField] private List<AudioClip> _sounds;
        
        [Header("PlayerHealth"), Space(10)]
        [SerializeField] private PlayerHealth _playerHealth;
        
        [Header("Attack Parameters"), Space(10)]
        [SerializeField] private GameObject _leftHandHitbox;
        [SerializeField] private GameObject _rightHandHitbox;
        
        // [Header("Attack Vfx"), Space(10)]
        // [SerializeField] private GameObject _unArmedflash;
        
        [Header("Animation Speed"), Space(10)] 
        [SerializeField] private float _anticipationSpeed = 2;
        [SerializeField] private float _activeSpeed = 2;
        [SerializeField] private float _recoverySpeed = 1;
        
        // [Header("FX"), Space(10)]
        // [SerializeField] private GameObject _katanaParticle;
     
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnStunAnimationStart()
        {
            AudioManager.Instance.PlaySound("Stun");
            Instantiate(_playerController.stunVfx, _playerController.transform.position + _playerController.stunVfxOffset, Quaternion.Euler(90, 0, 0));
        }
        
        public void OnStunAnimationEnd()
        {
            Debug.Log("Stun end");
            _playerController.isStunned = false;
            _playerHealth.CurrentStun = 0;
        }

        public void OnActiveLeftHandHitbox()
        {
            _leftHandHitbox.SetActive(true);
            // GameObject hitFlash = Instantiate(_unArmedflash, _rightHandHitbox.transform.position, Quaternion.identity);
            // hitFlash.transform.SetParent(_rightHandHitbox.transform);
            // hitFlash.transform.localRotation = Quaternion.Euler(20,-80,0);
        }

        public void OnActiveRightHandHitbox()
        {
            _rightHandHitbox.SetActive(true);
            // GameObject hitFlash = Instantiate(_unArmedflash, _leftHandHitbox.transform.position, Quaternion.Euler(20,-80,0));
            // hitFlash.transform.SetParent(_leftHandHitbox.transform);
            // hitFlash.transform.localRotation = Quaternion.Euler(20,-80,0);
        }
        
        public void OnDeactivateLeftHandHitbox()
        {
            _leftHandHitbox.SetActive(false);
        }

        public void OnDeactivateRightHandHitbox()
        {
            _rightHandHitbox.SetActive(false);
        }

        public void OnAttackAnimationEnd()
        {
            _playerController.attackOver = true;
        }

        public void OnHammerHitGround()
        {
            _playerController.gameObject.GetComponentInChildren<Hammer>().OnAttack();
        }

        public void OnHammerLeavesGround()
        {
            
        }

        public void OnHammerAttackOver()
        {
            _playerController.gameObject.GetComponentInChildren<Hammer>().DestroyHammer();
        }

        public void OnFeetHitGround()
        {
            AudioManager.Instance.PlaySound("Step1", UnityEngine.Random.Range(0.1f, 1.9f));
            GameObject vfx = Instantiate(_playerController.walkVfx,
                _playerController.transform.position + _playerController.walkVfxOffset, 
                Quaternion.identity);
            vfx.transform.forward = transform.forward;
        }
    }
}