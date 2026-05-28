using AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerController : MonoBehaviour
    {
        //ScriptableObject Reference
        [FormerlySerializedAs("PlayerStats")] public PlayerStats playerStats;

        //Character parts
        [Header("Character hitboxes")] [Space(4)] 
        [FormerlySerializedAs("_handSocket")] [SerializeField] private GameObject handSocket;
        
        // FSM 
        private PlayerStateMachine _currentState;
        
        //FSM Transitions
        [HideInInspector] public bool canAttack;
        [HideInInspector] public bool canSlide;
        [HideInInspector] public bool doSlide;
         public bool canJump;
         public bool jumped;
        [HideInInspector] public bool doAttackRightHand;
        [HideInInspector] public bool doAttackLeftHand;
        [HideInInspector] public bool attackOver;
        [HideInInspector] public bool isStunned;
        [HideInInspector] public bool isDead;
        [HideInInspector] public bool isDrowned;

        private float _timeSinceSlideInCd;
        
        //States
        [FormerlySerializedAs("_isGrounded")] [SerializeField] private bool isGrounded;

        //Components
        [FormerlySerializedAs("_playerHealth")]
        [Header("Animator")] [Space(4)] 
        [SerializeField] private PlayerHealth playerHealth;
        [FormerlySerializedAs("CharacterAnimator")] public Animator characterAnimator;

        [FormerlySerializedAs("CurrentWeapon")] [HideInInspector] public Weapon currentWeapon;
        private ConfigurableJoint _mainJoint;

        //VFX
        [FormerlySerializedAs("_fallSmoke")]
        [Header("VFX")] [Space(4)]
        [SerializeField] private GameObject fallSmoke;
        [FormerlySerializedAs("_fallSmokeOffset")] [SerializeField] private Vector3 fallSmokeOffset;
        
        public GameObject walkVfx;
        public Vector3 walkVfxOffset;
        public Vector3 walkVfxRotation;
        
        [FormerlySerializedAs("StunVfx")] public GameObject stunVfx;
        [FormerlySerializedAs("StunVfxOffset")] public Vector3 stunVfxOffset;
        
        [FormerlySerializedAs("_deathVfx")] [SerializeField] private GameObject deathVfx;
        [FormerlySerializedAs("_deathVfxOffset")] [SerializeField] private Vector3 deathVfxOffset;
        
        [FormerlySerializedAs("_drownedVfx")] [SerializeField] private GameObject drownedVfx;
        [FormerlySerializedAs("_drownedVfxOffset")] [SerializeField] private Vector3 drownedVfxOffset;
        
        //Inputs
        [FormerlySerializedAs("MoveInput")] public Vector2 moveInput;
        [FormerlySerializedAs("Rb")] [HideInInspector] public Rigidbody rb;
        
        //PauseMenu
        [FormerlySerializedAs("_pauseMenu")]
        [Header("PauseMenu Reference")] [Space(4)] 
        [SerializeField] private GameObject pauseMenu;
        
        //Syncing of physics objects
        SyncPhysicsObject[] _syncPhysicsObjects;

        public bool IsGrounded {
            get =>isGrounded;

            set { isGrounded = value; }
        }
        
        public PlayerStateMachine CurrentState => _currentState;
        
        public bool WeaponEquipped => currentWeapon != null;
        
        private void Awake()
        {
            _syncPhysicsObjects = GetComponentsInChildren<SyncPhysicsObject>();
            rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();

            //SO reset
            playerStats.ItemPickedUp = false;
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddPlayer(gameObject);
            }
            _currentState = new IdleState();
            _currentState.OnStateEnter(this);
        }

        private void Update()
        {
            //extra gravity to make the character less floaty
            if (!isGrounded) rb.AddForce(Vector3.down * playerStats.AdditionalGravity);

            // look towards the direction we want to move
            var inputMagnitude = moveInput.magnitude;

            if (inputMagnitude != 0)
            {
                var desiredDirection =
                    Quaternion.LookRotation(new Vector3(moveInput.x, 0, moveInput.y * -1), transform.up);

                // rotate target towards direction
                _mainJoint.targetRotation = Quaternion.RotateTowards(_mainJoint.targetRotation, desiredDirection,
                    Time.fixedDeltaTime * playerStats.RotationSpeed);
            }

            // slide cooldown
            if (!canSlide)
            {
                _timeSinceSlideInCd += Time.deltaTime;
                if (_timeSinceSlideInCd >= playerStats.SlideCd)
                {
                    canSlide = true;
                    _timeSinceSlideInCd = 0;
                }
            }
            
            if (currentWeapon != null) {
                currentWeapon.AutoUse(this);
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

            // Death gestion
            if (isDead)
            {
                Instantiate(deathVfx, transform.position + deathVfxOffset, Quaternion.Euler(-90, 0, 0));
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RemovePlayer(gameObject);
                }
                playerHealth.Dies();
                Destroy(gameObject);
            }

            if (isDrowned)
            {
                Instantiate(drownedVfx, transform.position + drownedVfxOffset, Quaternion.Euler(-90, 0, 0));
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.RemovePlayer(gameObject);
                }
                playerHealth.Dies();
                Destroy(gameObject);
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _syncPhysicsObjects.Length; i++)
            {
                _syncPhysicsObjects[i].UpdateJointFromAnimation();
            }
        }

        private void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        private void OnJump()
        {
            if (isGrounded && canJump)
            {
                rb.AddForce(Vector3.up * playerStats.JumpForceModifier, ForceMode.Impulse);
                jumped = true;
                isGrounded = false;
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
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
        }

        public void Equip(Weapon weapon)
        {
            currentWeapon = weapon;
            currentWeapon.Equip(this);
            weapon.GetComponent<ItemPickUp>().enabled = false;
            weapon.transform.parent = handSocket.transform;
            weapon.transform.localPosition = Vector3.zero;
            // Changer la rotation en fonction de l'objet équipé
            if (weapon is HammerAttack)
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(-30, 340, 160));
            }

            if (weapon is BazookaAttack)
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 90));
            }
            else
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
            }
        }

        public void UnEquip()
        {
            currentWeapon.Equip(null);
        }

        public void Move(float speedModifier)
        {
            rb.linearDamping = speedModifier / playerStats.MaxSpeed;
            rb.AddForce(new Vector3(moveInput.x * speedModifier, 0,
                moveInput.y * speedModifier) * -1);
        }
        
        public void GravityModification(float gravityModifier)
        {
            rb.AddForce(Vector3.down * gravityModifier);
        }
    }
}