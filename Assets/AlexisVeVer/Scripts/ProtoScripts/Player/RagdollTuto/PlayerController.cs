using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerController : MonoBehaviour
    {
        //Components
        private Animator _animator;
        private Rigidbody _rb;
        private ConfigurableJoint _mainJoint;
         
        //Inputs
        private Vector2 _moveInput;

        //ScriptableObject Reference
        [SerializeField] private PlayerStats _playerStats;
        
        //Character parts
        [Header("Character hitboxes"), Space(4)]
        [SerializeField] private GameObject _attackHitboxLeftHand;
        [SerializeField] private GameObject _attackHitboxRightHand;

        public bool LeftHandGrab;
        public bool RightHandGrab;
        public bool LeftHandReleaseGrab;
        public bool RightHandReleaseGrab;
        
        //States
        public bool IsGrounded;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();
        }

        private void Start()
        {
            _rb.linearDamping = _playerStats.SpeedModifier / _playerStats.MaxSpeed;
        }

        void Update()
        {
            //extra gravity to make the character less floaty
            if (!IsGrounded)
            {
                _rb.AddForce(Vector3.down * _playerStats.AdditionalGravity);
            }
            
            // look towards the direction we want to move
            float inputMagnitude = _moveInput.magnitude;

            if (inputMagnitude != 0)
            {
                Quaternion desiredDirection = Quaternion.LookRotation(new Vector3(_moveInput.x, 0, _moveInput.y * - 1), transform.up);
                
                // rotate target towards direction
                _mainJoint.targetRotation = Quaternion.RotateTowards(_mainJoint.targetRotation, desiredDirection, Time.fixedDeltaTime * _playerStats.RotationSpeed);
            }
            
            // move
            _rb.AddForce(new Vector3(_moveInput.x * _playerStats.SpeedModifier, 0, _moveInput.y * _playerStats.SpeedModifier) * -1); 
        }
        
        private void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            if (IsGrounded)
            {
                _rb.AddForce(Vector3.up * _playerStats.JumpForceModifier, ForceMode.Impulse);
                IsGrounded = false;
            }
        }

        private void OnLeftHandGrab()
        {
            LeftHandGrab = true;
        }

        private void OnRightHandGrab()
        {
            RightHandGrab = true;
        }
        
        private void OnLeftHandReleaseGrab()
        {
            LeftHandReleaseGrab = true;
        }

        private void OnRightHandReleaseGrab()
        {
            RightHandReleaseGrab = true;
        }

        private void OnLeftHandAttack()
        {
            _attackHitboxLeftHand.SetActive(true);
        }

        private void OnRightHandAttack()
        {
            _attackHitboxRightHand.SetActive(true);
        }
        
        private void OnSlide()
        {
            _rb.AddForce(new Vector3(_rb.linearVelocity.x * _playerStats.SlideForceMultiplier, 0, _rb.linearVelocity.z * _playerStats.SlideForceMultiplier), ForceMode.VelocityChange);
        }
    }
}