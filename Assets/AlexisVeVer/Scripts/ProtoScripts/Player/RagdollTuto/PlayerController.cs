using AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using AlexisVeVer.Scripts.UI;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerController : MonoBehaviour
    {
        //ScriptableObject Reference
        public PlayerStats PlayerStats;

        //Character parts
        [Header("Character hitboxes")] [Space(4)] 
        [SerializeField] private GameObject _attackHitboxLeftHand;
        [SerializeField] private GameObject _handSocket;
        [SerializeField] private GameObject _attackHitboxRightHand;
        
        // FSM 
        private PlayerStateMachine _currentState;
        
        //FSM Transitions
        [HideInInspector] public bool canAttack;
        [HideInInspector] public bool canSlide;
        [HideInInspector] public bool doSlide;
        [HideInInspector] public bool canJump;
        [HideInInspector] public bool doAttackRightHand;
        [HideInInspector] public bool doAttackLeftHand;
        [HideInInspector] public bool attackOver;
        [HideInInspector] public bool isStunned;
        [HideInInspector] public bool isDead;

        private float _timeSinceSlideInCd;
        
        //States
        private bool _isGrounded;

        //Components
        [Header("Animator")] [Space(4)] 
        public Animator CharacterAnimator;

        [HideInInspector] public Weapon CurrentWeapon;
        private ConfigurableJoint _mainJoint;

        //VFX
        [Header("VFX")] [Space(4)] 
        public GameObject FallSmoke;
        public GameObject StunVfx;
        public GameObject DeathVfx;
        
        
        //Inputs
        public Vector2 MoveInput;
        [HideInInspector] public Rigidbody Rb;
        
        //PauseMenu
        [Header("PauseMenu Reference")] [Space(4)] 
        [SerializeField] private GameObject _pauseMenu;
        
        public bool IsGrounded {
            get =>_isGrounded;

            set { _isGrounded = value; }
        }
        
        public bool WeaponEquipped => CurrentWeapon != null;
        
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();

            //SO reset
            PlayerStats.ItemPickedUp = false;
        }

        private void Start()
        {
            _currentState = new IdleState();
            _currentState.OnStateEnter(this);
        }

        private void Update()
        {
            //extra gravity to make the character less floaty
            if (!_isGrounded) Rb.AddForce(Vector3.down * PlayerStats.AdditionalGravity);

            // look towards the direction we want to move
            var inputMagnitude = MoveInput.magnitude;

            if (inputMagnitude != 0)
            {
                var desiredDirection =
                    Quaternion.LookRotation(new Vector3(MoveInput.x, 0, MoveInput.y * -1), transform.up);

                // rotate target towards direction
                _mainJoint.targetRotation = Quaternion.RotateTowards(_mainJoint.targetRotation, desiredDirection,
                    Time.fixedDeltaTime * PlayerStats.RotationSpeed);
            }

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
            
            if (CurrentWeapon != null) {
                CurrentWeapon.AutoUse(this);
            }
            
            
            
            // FSM Gestion
            if (_currentState != null)
            {
                // Update Methods
                _currentState.OnUpdate(this);
            
                //State Switching
                PlayerStateMachine nextBaseState = _currentState.NextState(this);
                if (nextBaseState != null)
                {
                    _currentState.OnStateExit(this); 
                    _currentState = nextBaseState; 
                    _currentState.OnStateEnter(this);
                }
            }
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
            doAttackLeftHand = true;
        }

        private void OnRightHandAttack()
        {
            doAttackRightHand = true;
        }

        private void OnSlide()
        {
            if (canSlide)
            {
                doSlide = true;
            }
        }

        private void OnPauseGame()
        {
            if (_pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
            }
        }

        public void Equip(Weapon weapon)
        {
            CurrentWeapon = weapon;
            CurrentWeapon.Equip(this);
            weapon.GetComponent<ItemPickUp>().enabled = false;
            weapon.transform.parent = _handSocket.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
        }

        public void UnEquip()
        {
            CurrentWeapon.Equip(null);
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