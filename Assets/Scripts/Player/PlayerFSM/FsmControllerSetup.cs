using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.PlayerFSM
{
    public class FsmControllerSetup : MonoBehaviour
    {
        //ScriptableObject Reference
        public PlayerStats PlayerStats;

        //Character parts
        [Header("Character hitboxes")] [Space(4)] 
        [SerializeField] private GameObject _attackHitboxLeftHand;
        [SerializeField] private GameObject _attackHitboxRightHand;
        
        // FSM 
        private PlayerStateMachine _currentState;
        
        //FSM Transitions
        public bool canAttack;
        public bool canSlide;
        public bool doSlide;
        public bool canJump;

        private float _timeSinceSlideInCd;
        
        //States
        private bool _isGrounded;
        

        //Inputs
        public Vector2 MoveInput;
        public Rigidbody Rb;

        public bool IsGrounded {
            get =>_isGrounded;

            set { _isGrounded = value; }
        }
        
        
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            // _currentState = new IdleState();
            // _currentState.OnStateEnter(this);
        }

        private void Update()
        {
            //extra gravity to make the character less floaty
            

            // slide cooldown
            if (!canSlide)
            {
                _timeSinceSlideInCd += Time.deltaTime;
                if (_timeSinceSlideInCd >= PlayerStats.SlideCd)
                {
                    canSlide = true;
                    _timeSinceSlideInCd = 0;
                }
            }
            
            // FSM Gestion
            // if (_currentState != null)
            // {
            //     // Update Methods
            //     _currentState.OnUpdate(this);
            //
            //     //State Switching
            //     PlayerStateMachine nextBaseState = _currentState.NextState(this);
            //     if (nextBaseState != null)
            //     {
            //         _currentState.OnStateExit(this); 
            //         _currentState = nextBaseState; 
            //         _currentState.OnStateEnter(this);
            //     }
            // }
        }

        private void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            if (_isGrounded)
            {
                Rb.AddForce(Vector3.up * PlayerStats.JumpForceModifier, ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        private void OnLeftHandAttack()
        {
            // if (WeaponEquipped)
            // {
            //     CurrentWeapon.Use(this);
            // }
            // else
            // {
            //     _attackHitboxLeftHand.SetActive(true);
            // }
        }

        private void OnRightHandAttack()
        {
            // if (WeaponEquipped)
            // {
            //     CurrentWeapon.Use(this);
            // }
            // else
            // {
            //     _attackHitboxRightHand.SetActive(true);
            // }
        }

        private void OnSlide()
        {
            if (canSlide)
            {
                doSlide = true;
            }
        }

        public void Move(float speedModifier)
        {
            Rb.linearDamping = speedModifier / PlayerStats.MaxSpeed;
            Rb.AddForce(new Vector3(MoveInput.x * speedModifier, 0,
                MoveInput.y * speedModifier) * -1);
        }

        public void GravityModification(float gravityModifier)
        {
            Rb.AddForce(Vector3.down * gravityModifier);
        }
    }
}