using UnityEngine;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private GameObject _playerController;

        [SerializeField] private Animator _animator;
        [SerializeField] private Image _healthBar;

        [SerializeField] private float _animationTime;
        public float CurrentHealth;
        public float CurrentStun;

        public bool GotHit;
        public bool IsStunned;
        public bool IsDead;

        private void Awake()
        {
            CurrentHealth = _playerStats.MaxHealth;
            CurrentStun = _playerStats.MinStun;
        }

        private void Update()
        {
            if (CurrentStun >= _playerStats.MaxStun)
            {
                IsStunned = true;
                GetStunned();
            }

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                Dies();
            }

            // _healthBar.fillAmount = _currentHealth / _playerStats.MaxHealth;
        }

        public void GetHit(float damage, float stun)
        {
            GotHit = true;
            CurrentHealth -= damage;
            CurrentStun += stun;
            Debug.Log("J'ai été touché. Ma vie est désormais de " + _playerStats.CurrentHealth +
                      " et ma valeur de stun est désormais de " + _playerStats.CurrentStun);
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
            // CurrentStun = _playerStats.MinStun;
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
        }

        [ContextMenu("TakeDamage")]
        private void TakeDamage()
        {
            GetHit(1, 2);
        }
    }
}
