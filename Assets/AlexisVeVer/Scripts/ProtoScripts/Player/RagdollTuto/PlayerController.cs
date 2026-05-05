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

        //ControllerSettings
        [Header("Movements parameters"), Space(4)]
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _jumpForceModifier;
        [SerializeField] private float _additionalGravity = 10;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _slideForceMultiplier;
        
        //Character parts
        [Header("Character hitboxes"), Space(4)]
        [SerializeField] private SphereCollider _grabHitboxLeftHand;
        [SerializeField] private SphereCollider _grabHitboxRightHand;
        [SerializeField] private GameObject _attackHitboxLeftHand;
        [SerializeField] private GameObject _attackHitboxRightHand;
        
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
            _rb.linearDamping = _speedModifier / _maxSpeed;
        }

        void Update()
        {
            //extra gravity to make the character less floaty
            if (!IsGrounded)
            {
                _rb.AddForce(Vector3.down * _additionalGravity);
            }
            
            // look towards the direction we want to move
            float inputMagnitude = _moveInput.magnitude;

            if (inputMagnitude != 0)
            {
                Quaternion desiredDirection = Quaternion.LookRotation(new Vector3(_moveInput.x, 0, _moveInput.y * - 1), transform.up);
                
                // rotate target towards direction
                _mainJoint.targetRotation = Quaternion.RotateTowards(_mainJoint.targetRotation, desiredDirection, Time.fixedDeltaTime * _rotationSpeed);
            }
            
            // move
            _rb.AddForce(new Vector3(_moveInput.x * _speedModifier, 0, _moveInput.y * _speedModifier) * -1); 
        }
        
        private void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            if (IsGrounded)
            {
                _rb.AddForce(Vector3.up * _jumpForceModifier, ForceMode.Impulse);
                IsGrounded = false;
            }
        }

        private void OnLeftHandGrab()
        {
            _grabHitboxLeftHand.enabled = true;
        }

        private void OnRightHandGrab()
        {
            _grabHitboxRightHand.enabled = true;
        }

        private void OnLeftHandAttack()
        {
            _attackHitboxLeftHand.SetActive(true);
        }

        private void OnRightHandAttack()
        {
            _attackHitboxRightHand.SetActive(true);
        }
        
        private void OnShoulderReleased()
        {
            _grabHitboxLeftHand.enabled = false;
            _grabHitboxRightHand.enabled = false;
        }

        private void OnSlide()
        {
            _rb.AddForce(new Vector3(_rb.linearVelocity.x * _slideForceMultiplier, 0, _rb.linearVelocity.z * _slideForceMultiplier), ForceMode.VelocityChange);
        }
    }
}