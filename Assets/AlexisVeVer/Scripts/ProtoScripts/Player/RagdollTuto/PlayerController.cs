using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private Rigidbody _rb;
        private ConfigurableJoint _mainJoint;
         
        //Inputs
        private Vector2 _moveInput;

        //ControllerSettings
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _jumpForceModifier;
        [SerializeField]  private float _additionalGravity = 10;
        [SerializeField] private float _maxSpeed;
        
        //States
        public bool IsGrounded;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
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
            _rb.linearVelocity = new Vector3(_moveInput.x * _speedModifier, 0, _moveInput.y * _speedModifier) * -1;
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
    }
}
