using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

namespace Player
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
        
        [Header("Attack Vfx"), Space(10)]
        [SerializeField] private GameObject _unArmedflash;
        [SerializeField] private GameObject _leftHandHitboxVisual;
        [SerializeField] private GameObject _rightHandHitboxVisual;

        [SerializeField] private Vector3 _leftHitFlashOffset;
        [SerializeField] private Vector3 _rightHitFlashOffset;
        
        [SerializeField] private GameObject _katanaVfx;
        
        [Header("Animation Speed"), Space(10)] 
        [SerializeField] private float _anticipationSpeed = 2;
        [SerializeField] private float _activeSpeed = 2;
        [SerializeField] private float _recoverySpeed = 1;
     
        private GameObject _stunVfx;
        private Animator _animator;

        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnStunAnimationStart()
        {
            AudioManager.Instance.PlaySound("Stun");
            _stunVfx = Instantiate(_playerController.stunVfx, _playerController.transform.position + _playerController.stunVfxOffset, Quaternion.Euler(90, 0, 0));
            _stunVfx.transform.parent = _playerController.transform;
        }
        
        public void OnStunAnimationEnd()
        {
            _playerController.isStunned = false;
            _playerHealth.CurrentStun = 0;
            Destroy(_stunVfx);
        }

        public void OnActiveLeftHandAttack()
        {
            GameObject hitFlash = Instantiate(_unArmedflash, _playerController.transform.position, Quaternion.identity);
            hitFlash.transform.parent = _playerController.transform;
            hitFlash.transform.localPosition = _leftHitFlashOffset; 
            hitFlash.transform.localRotation = Quaternion.Euler(260, 55, -30);
        }

        public void OnActiveRightHandAttack()
        {
            GameObject hitFlash = Instantiate(_unArmedflash, _playerController.transform.position, Quaternion.identity);
            hitFlash.transform.parent = _playerController.transform;
            hitFlash.transform.localPosition = _rightHitFlashOffset;
            hitFlash.transform.localRotation = Quaternion.Euler(110, 55, -140);
        }
        
        public void OnActiveLeftHandHitbox()
        {
            _leftHandHitbox.SetActive(true);
        }

        public void OnActiveRightHandHitbox()
        {
            _rightHandHitbox.SetActive(true);
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

        public void OnActiveKatanaSlash()
        { 
            GameObject KatanaVfx = Instantiate(_katanaVfx, _rightHandHitboxVisual.transform.position, Quaternion.identity);
            KatanaVfx.transform.parent = _rightHandHitboxVisual.transform;
            KatanaVfx.transform.localRotation = Quaternion.Euler(20,-80,0);
            KatanaVfx.transform.parent = null;
        }
        
      
    }
}