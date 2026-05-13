using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using UnityEngine;
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

        public bool LeftHandGrab;
        public bool RightHandGrab;
        public bool LeftHandReleaseGrab;
        public bool RightHandReleaseGrab;

        //States
        private bool _isGrounded;

        //Components
        private Animator _animator;

        private Weapon _currentWeapon;
        private ConfigurableJoint _mainJoint;

        //Inputs
        public Vector2 MoveInput;
        private Rigidbody _rb;

        public bool IsGrounded {
            get =>_isGrounded;

            set { _isGrounded = value; }
        }
        
        public bool WeaponEquipped => _currentWeapon != null;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _mainJoint = GetComponent<ConfigurableJoint>();

            //SO reset
            PlayerStats.CanSlide = true;
            PlayerStats.TimeFromSlide = 0;
            PlayerStats.ItemPickedUp = false;
        }

        private void Start()
        {
            _rb.linearDamping = PlayerStats.SpeedModifier / PlayerStats.MaxSpeed;
        }

        private void Update()
        {
            //extra gravity to make the character less floaty
            if (!_isGrounded) _rb.AddForce(Vector3.down * PlayerStats.AdditionalGravity);

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

            // move
            _rb.AddForce(new Vector3(MoveInput.x * PlayerStats.SpeedModifier, 0,
                MoveInput.y * PlayerStats.SpeedModifier) * -1);

            // slide cooldown
            if (!PlayerStats.CanSlide)
            {
                PlayerStats.TimeFromSlide += Time.deltaTime;
                if (PlayerStats.TimeFromSlide >= PlayerStats.SlideCd)
                {
                    PlayerStats.CanSlide = true;
                    PlayerStats.TimeFromSlide = 0;
                }
            }
            
            if (_currentWeapon != null) {
                _currentWeapon.AutoUse(this);
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
                _rb.AddForce(Vector3.up * PlayerStats.JumpForceModifier, ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        private void OnLeftHandGrab()
        {
            if (!WeaponEquipped) LeftHandGrab = true;
        }

        private void OnRightHandGrab()
        {
            if (!WeaponEquipped) RightHandGrab = true;
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
            if (WeaponEquipped)
            {
                _currentWeapon.Use(this);
            }
            else
            {
                _attackHitboxLeftHand.SetActive(true);
            }
        }

        private void OnRightHandAttack()
        {
            if (WeaponEquipped)
            {
                _currentWeapon.Use(this);
            }
            else
            {
                _attackHitboxRightHand.SetActive(true);
            }
        }

        private void OnSlide()
        {
            if (PlayerStats.CanSlide)
            {
                _rb.AddForce(
                    new Vector3(_rb.linearVelocity.x * PlayerStats.SlideForceMultiplier, 0,
                        _rb.linearVelocity.z * PlayerStats.SlideForceMultiplier),
                    ForceMode.VelocityChange);
                PlayerStats.CanSlide = false;
            }
        }

        public void Equip(Weapon weapon)
        {
            _currentWeapon = weapon;
            _currentWeapon.Equip(this);
            weapon.GetComponent<ItemPickUp>().enabled = false;
            weapon.transform.parent = _handSocket.transform;
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
        }

        public void UnEquip()
        {
            _currentWeapon.Equip(null);
        }
    }
}