using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AlexisVeVer.Scripts.UI
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField] PlayerStats playerStats;
        [SerializeField]private PlayerHealth playerHealth;
        
        private Image _healthBar;
        
        void Awake()
        {
            _healthBar = GetComponent<Image>();
        }
        
        void Update()
        {
            if (playerHealth != null)
            {
                _healthBar.fillAmount = playerHealth.CurrentHealth / playerStats.MaxHealth;
            }
        }
    }
}
