using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player
{
    public class AnimationEvents : MonoBehaviour
    {
        [Header("PlayerController")]
        [SerializeField] private PlayerController _playerController;
        
        [Header("Attack Parameters"), Space(10)]
        [SerializeField] private GameObject _leftHandHitbox;
        [SerializeField] private GameObject _rightHandHitbox;
     
        private Animator _animator;

        private void Start()
        {
            
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
            if (_playerController.doAttackLeftHand)
            {
                _playerController.doAttackLeftHand = false;
            }
            
            if (_playerController.doAttackRightHand)
            {
                _playerController.doAttackRightHand = false;
            }
        }

        public void OnStunAnimationEnd()
        {
            _playerController.playerHealth.IsStunned = false;
        }
        
        public void OnDeadAnimationEnd()
        {
            Destroy(_playerController.gameObject, 0.5f);
        }
    }
}