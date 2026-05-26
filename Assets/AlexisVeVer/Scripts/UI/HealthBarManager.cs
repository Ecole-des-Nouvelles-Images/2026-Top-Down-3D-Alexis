using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.UI
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private PlayerHealth _playerHealth;
        
        private Image _healthBar;
        
        void Awake()
        {
            _healthBar = GetComponent<Image>();
        }

        private void Start()
        {
        }

        void Update()
        {
            if (_playerHealth != null)
            {
                _healthBar.fillAmount = _playerHealth.CurrentHealth / _playerStats.MaxHealth;
            }

            else
            {
                _healthBar.fillAmount = 0;
            }
        }
    }
}
