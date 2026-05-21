using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class AttackDamage : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().GetHit(_playerStats.AttackDamage, _playerStats.AttackStun);
                gameObject.SetActive(false);
            }
        }
    }
}