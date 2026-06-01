using System;
using System.Collections.Generic;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using UnityEngine;
using Random = System.Random;

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
     
        private Animator _animator;

        private void Start()
        {
            
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
            _playerController.gameObject.GetComponentInChildren<HammerAttack>().OnAttack();
        }

        public void OnHammerLeavesGround()
        {
            _playerController.gameObject.GetComponentInChildren<HammerAttack>().EndAttack();
        }

        public void OnFeetHitGround()
        {
            AudioManager.Instance.PlaySound("Step1", UnityEngine.Random.Range(0.1f, 1.9f));
            Instantiate(_playerController.walkVfx,
                _playerController.transform.position + _playerController.walkVfxOffset, Quaternion.Euler(_playerController.walkVfxRotation + _playerController.transform.rotation.eulerAngles));
        }
    }
}
