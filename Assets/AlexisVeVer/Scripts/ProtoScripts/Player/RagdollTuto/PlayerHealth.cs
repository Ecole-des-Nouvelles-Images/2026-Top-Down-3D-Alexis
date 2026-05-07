using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private GameObject _playerController;
        
        [SerializeField] private Animator _animator;
        
        [SerializeField] private float _animationTime;

        private void Awake()
        {
            _playerStats.CurrentHealth = _playerStats.MaxHealth;
            _playerStats.CurrentStun = _playerStats.MinStun;
        }

        private void Update()
        {
            if (_playerStats.CurrentStun >= _playerStats.MaxStun)
            {
                GetStunned();
            }

            if (_playerStats.CurrentHealth <= 0)
            {
                Dies();
            }
        }

        public void GetHit(float damage, float stun)
        {
            _playerStats.CurrentHealth -= damage;
            _playerStats.CurrentStun += stun;
            Debug.Log("J'ai été touché. Ma vie est désormais de " + _playerStats.CurrentHealth + " et ma valeur de stun est désormais de " +  _playerStats.CurrentStun);
        }

        private void GetStunned()
        {
            _playerController.GetComponent<PlayerController>().enabled = false;
            float stunTime = 0;
            stunTime += Time.deltaTime;
            if (stunTime >= _playerStats.StunDuration)
            {
                _playerController.GetComponent<PlayerController>().enabled = true;
            }
            _playerStats.CurrentStun = _playerStats.MinStun;
        }

        private void Dies()
        {
            _playerController.GetComponent<PlayerController>().enabled = false;
            Debug.Log("Player is Dead");
            // Animation de mort
            float timeBeforeDestroy = 0;
            timeBeforeDestroy += Time.deltaTime;
            if (timeBeforeDestroy > _animationTime)
            {
                Destroy(_playerController);
            }
        }
    }
}
