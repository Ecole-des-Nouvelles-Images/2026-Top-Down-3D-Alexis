using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ConfigurableJoint _mainJoint;
        
        //Inputs
        private Vector2 _moveInput;
        private bool _jumpPressed;

        //ControllerSettings
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _rotationSpeed;
        [SerializeField]  private float _additionalGravity = 10;
        [SerializeField] private float _maxSpeed;
        
        //States
        private bool _isGrounded;
        
        //Raycasts
        private RaycastHit[] _raycastHits = new RaycastHit[10];

        public PlayerController(bool jumpPressed)
        {
            _jumpPressed = jumpPressed;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();
        }

        // Update is called once per frame
        void Update()
        {
            _rb.linearVelocity = new Vector3(_moveInput.x * _speedModifier, 0, _moveInput.y * _speedModifier) * -1;
        }

        void FixedUpdate()
        {
            //assume we are not grounded
            _isGrounded = false;
            
            //Check if we are grounded
            int numberOfHits =
                Physics.SphereCastNonAlloc(_rb.position, 0.1f, transform.up * -1, _raycastHits, 0.5f);
            
            //Check if results are acceptable
            for (int i = 0; i < numberOfHits; i++)
            {
                //Ignore self hits
                if (_raycastHits[i].transform.root == transform)
                {
                    continue;
                }
                
                _isGrounded = true;

                break;
            }
            //extra gravity to make the character less floaty
            if (!_isGrounded)
            {
                _rb.AddForce(Vector3.down * _additionalGravity);
            }
            
            // look towards the direction we want to move
            float inputMagnitude = _moveInput.magnitude;

            if (inputMagnitude != 0)
            {
                Quaternion desiredDirection = Quaternion.LookRotation(new Vector3(_moveInput.x, 0, _moveInput.y * - 1), transform.up);
                
                //rotate target towards direction
                _mainJoint.targetRotation = Quaternion.RotateTowards(_mainJoint.targetRotation, desiredDirection, Time.fixedDeltaTime * _rotationSpeed);
            }


        }
        
        private void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            _jumpPressed = true;
        }
    }
}
