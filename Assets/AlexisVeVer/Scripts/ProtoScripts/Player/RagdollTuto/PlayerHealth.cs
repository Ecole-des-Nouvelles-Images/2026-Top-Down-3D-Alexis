using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private GameObject skinnedMeshHolder;
        
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Animator animator;
        private Material skinnedMeshRendererMaterial;
        
        [SerializeField] private float animationTime;
        private float _currentHealth;
        private float _currentStun;

        private float _timeInFlash;
        [SerializeField] private float _flashTime = 0.01f;

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
        }

        private void Update()
        {
            if (_currentStun >= _playerStats.MaxStun)
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
                if (_timeInFlash >= animationTime)
                {
                    skinnedMeshRendererMaterial.SetFloat("_HitIntensity", 0);
                    _timeInFlash = 0;
                }
            }
        }

        public void GetHit(float damage, float stun)
        {
            _currentHealth -= damage;
            _currentStun += stun;
            skinnedMeshRendererMaterial.SetFloat("_HitIntensity", 0.7f);
        }

        private void GetStunned()
        {
            _playerController.isStunned = true;
        }

        private void Dies()
        {
            _playerController.isDead = true;
        }

        [ContextMenu("TakeDamage")]
        private void TakeDamage()
        {
            GetHit(15, 0);
        }
    }
}