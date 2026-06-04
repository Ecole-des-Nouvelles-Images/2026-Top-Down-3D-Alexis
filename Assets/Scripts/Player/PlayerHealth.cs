using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject skinnedMeshHolder;
        
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Animator animator;
        private Rigidbody rigidbody;
        private Material skinnedMeshRendererMaterial;
        
        [SerializeField] private float animationTime;
        private float _currentHealth;
        private float _currentStun;

        private float _timeInFlash;
        [SerializeField] private float _flashTime = 0.001f;

        public float CurrentHealth => _currentHealth;
        public float CurrentStun
        {
            get => _currentStun;
            set => _currentStun = value;
        }

        private void Awake()
        {
            _currentHealth = playerStats.MaxHealth;
            _currentStun = playerStats.MinStun;
            skinnedMeshRendererMaterial = skinnedMeshHolder.GetComponent<Renderer>().materials[1];
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_currentStun >= playerStats.MaxStun)
            {
                GetStunned();
            }

            if (_currentHealth <= 0)
            {
                Dies();
            }

            if (skinnedMeshRendererMaterial.GetFloat("_HitIntensity") > 0)
            {
                _timeInFlash += Time.deltaTime;
                if (_timeInFlash >= _flashTime)
                {
                    skinnedMeshRendererMaterial.SetFloat("_HitIntensity", 0);
                    _timeInFlash = 0;
                }
            }
        }
        
        //, Vector3 knockBack

        public void GetHit(float damage, float stun, float knockBackForce, GameObject attacker)
        {
            AudioManager.Instance.PlaySound("Slap");
            _currentHealth -= damage;
            _currentStun += stun;
            skinnedMeshRendererMaterial.SetFloat("_HitIntensity", 0.7f);
            rigidbody.AddForce(new Vector3(transform.position.x - attacker.transform.position.x, 0,
                transform.position.z - attacker.transform.position.z) * knockBackForce, ForceMode.Impulse);
        }

        private void GetStunned()
        {
            playerController.isStunned = true;
        }

        public void Dies()
        {
            playerController.isDead = true;
            _currentHealth = 0;
            AlexisVeVer.Scripts.GameManager.Instance.PlayerDead(gameObject);
        }
    }
}