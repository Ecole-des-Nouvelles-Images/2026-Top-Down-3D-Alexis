using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerHealth : MonoBehaviour
    {
        [FormerlySerializedAs("_playerStats")] [SerializeField] private PlayerStats playerStats;
        [FormerlySerializedAs("_playerController")] [SerializeField] private PlayerController playerController;
        
        [FormerlySerializedAs("_animator")] [SerializeField] private Animator animator;
        [FormerlySerializedAs("_skinnedMeshRendererMaterial")] [SerializeField] private Material skinnedMeshRendererMaterial;
        
        [FormerlySerializedAs("_animationTime")] [SerializeField] private float animationTime;
        private float _currentHealth;
        private float _currentStun;

        private float _timeInFlash;
        private float _flashTime = 0.5f;

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

            if (skinnedMeshRendererMaterial.GetFloat("HitIntensity") > 0)
            {
                _timeInFlash += Time.deltaTime;
                if (_timeInFlash >= animationTime)
                {
                    skinnedMeshRendererMaterial.SetFloat("HitIntensity", 0);
                    _timeInFlash = 0;
                }
            }
        }

        public void GetHit(float damage, float stun)
        {
            _currentHealth -= damage;
            _currentStun += stun;
            skinnedMeshRendererMaterial.SetFloat("HitIntensity", 0.7f);
        }

        private void GetStunned()
        {
            playerController.isStunned = true;
        }

        public void Dies()
        {
            playerController.isDead = true;
            _currentHealth = 0;
        }

        [ContextMenu("TakeDamage")]
        private void TakeDamage()
        {
            GetHit(2, 0);
        }
    }
}