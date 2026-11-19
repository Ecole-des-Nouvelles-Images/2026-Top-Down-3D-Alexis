using System;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public event EventHandler<PlayerController> OnDeath;
        
        public event EventHandler<float> OnHpChanged;
        
        //[SerializeField] private Renderer skinnedMeshHolder;
        
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Animator animator;
        [SerializeField] private PlayerStats _playerStats;
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
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            skinnedMeshRendererMaterial = playerController.PlayerVisual.skinnedMeshHolder.materials[1];
        }
        
        private void Update()
        {
            if (_currentStun >= playerStats.MaxStun)
            {
                GetStunned();
                _currentStun = playerStats.MinStun;
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

        public void GetHit(float damage, float stun, float knockBackForce)
        {
            //AudioManager.Instance.PlaySound("Slap");
            OnHpChanged?.Invoke(this, CurrentHealth / _playerStats.MaxHealth);
            _currentHealth -= damage;
            _currentStun += stun;
            skinnedMeshRendererMaterial.SetFloat("_HitIntensity", 0.7f);
            rigidbody.AddForce(- transform.forward * knockBackForce, ForceMode.Impulse);
        }

        private void GetStunned()
        {
            playerController.isStunned = true;
        }

        public void Dies()
        {
            OnDeath?.Invoke(this, playerController);
            playerController.isDead = true;
            _currentHealth = 0;
            AlexisVeVer.Scripts.GameManager.Instance.PlayerDead(gameObject);
        }
    }
}