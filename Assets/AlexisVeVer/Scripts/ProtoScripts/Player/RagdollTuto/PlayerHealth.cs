using UnityEngine;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerController _playerController;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _healthBar;
        
        [SerializeField] private float _animationTime;
        private float _currentHealth;
        private float _currentStun;

        public float CurrentHealth => _currentHealth;
        public float CurrentStun
        {
            get => _currentStun;
            set => _currentStun = value;
        }

        private void Awake()
        {
            _currentHealth = _playerStats.MaxHealth;
            _currentStun = _playerStats.MinStun;
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
            
            // _healthBar.fillAmount = _currentHealth / _playerStats.MaxHealth;
        }

        public void GetHit(float damage, float stun)
        {
            _currentHealth -= damage;
            _currentStun += stun;
            Debug.Log("J'ai été touché. Ma vie est désormais de " + _currentHealth + " et ma valeur de stun est désormais de " +  _currentStun);
        }

        private void GetStunned()
        {
            // _playerController.GetComponent<PlayerController>().enabled = false;
            // float stunTime = 0;
            // stunTime += Time.deltaTime;
            // if (stunTime >= _playerStats.StunDuration)
            // {
            //     _playerController.GetComponent<PlayerController>().enabled = true;
            // }
            _playerController.isStunned = true;
        }

        private void Dies()
        {
            // _playerController.GetComponent<PlayerController>().enabled = false;
            // Debug.Log("Player is Dead");
            // // Animation de mort
            // float timeBeforeDestroy = 0;
            // timeBeforeDestroy += Time.deltaTime;
            // if (timeBeforeDestroy > _animationTime)
            // {
            //     Destroy(_playerController);
            // }
            _playerController.isDead = true;
        }

        [ContextMenu("TakeDamage")]
        private void TakeDamage()
        {
            GetHit(3, 4);
        }
    }
}
