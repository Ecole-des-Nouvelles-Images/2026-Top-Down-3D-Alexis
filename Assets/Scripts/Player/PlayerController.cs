using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using Items;
using Player.PlayerFSM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        //ScriptableObject Reference
        [FormerlySerializedAs("PlayerStats")] public PlayerStats playerStats;

        //Character parts
        [Header("Character hitboxes")] [Space(4)] 
        [SerializeField] private GameObject handSocket;
        
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
        [SerializeField] private bool isGrounded;
        [HideInInspector] public bool IsTerrainMoving;

        //Components
        [Header("Animator")] [Space(4)] 
        [SerializeField] private PlayerHealth playerHealth;
        public Animator characterAnimator;

        [HideInInspector] public Weapon currentWeapon;
        private ConfigurableJoint _mainJoint;

        //VFX
        [Header("VFX")] [Space(4)]
        [SerializeField] private GameObject fallSmoke;
        [SerializeField] private Vector3 fallSmokeOffset;
        
        public GameObject walkVfx;
        public Vector3 walkVfxOffset;
        public Vector3 walkVfxRotation;
        
        public GameObject stunVfx;
        public Vector3 stunVfxOffset;
        
        [SerializeField] private GameObject deathVfx;
        [SerializeField] private Vector3 deathVfxOffset;
        
        [SerializeField] private GameObject drownedVfx;
        [SerializeField] private Vector3 drownedVfxOffset;
        
        //Inputs
         public Vector2 moveInput;
        [HideInInspector] public Rigidbody rb;
        
        //PauseMenu
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
            if (AlexisVeVer.Scripts.GameManager.Instance != null)
            {
                AlexisVeVer.Scripts.GameManager.Instance.AddPlayer(gameObject);
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
                AudioManager.Instance.PlaySound("PinguinDeath");
                Instantiate(deathVfx, transform.position + deathVfxOffset, Quaternion.Euler(-90, 0, 0));
                if (AlexisVeVer.Scripts.GameManager.Instance != null)
                {
                    AlexisVeVer.Scripts.GameManager.Instance.RemovePlayer(gameObject);
                }
                playerHealth.Dies();
                Destroy(gameObject);
            }

            if (isDrowned)
            {
                AudioManager.Instance.PlaySound("WaterSplash");
                Instantiate(drownedVfx, transform.position + drownedVfxOffset, Quaternion.Euler(-90, 0, 0));
                if (AlexisVeVer.Scripts.GameManager.Instance != null)
                {
                    AlexisVeVer.Scripts.GameManager.Instance.RemovePlayer(gameObject);
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
                rb.AddForce(Vector3.up * playerStats.JumpForceModifier * Time.deltaTime, ForceMode.Impulse);
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
            weapon.transform.parent = handSocket.transform;
            weapon.transform.localPosition = Vector3.zero;
            // Changer la rotation en fonction de l'objet équipé
            if (weapon is Hammer)
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, -90));
            }

            if (weapon is Bazooka)
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 90));
            }

            if (weapon is Katana)
            {
                weapon.transform.localRotation = Quaternion.Euler(new Vector3(-57, 46, -130));
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
            Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * (speedModifier * -1);
            rb.AddForce(movement * Time.deltaTime);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, playerStats.MaxSpeed);
        }
        
        public void GravityModification(float gravityModifier)
        {
            rb.AddForce(Vector3.down * (gravityModifier * Time.deltaTime));
        }
    }
}