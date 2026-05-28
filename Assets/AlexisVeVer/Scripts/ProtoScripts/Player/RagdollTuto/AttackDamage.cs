using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class AttackDamage : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;

        private float _timeActive;
        
        private void Update()
        {
            _timeActive += Time.deltaTime;
            if (_timeActive > _playerStats.AttackDuration)
            {
                _timeActive = 0f;
                gameObject.SetActive(false);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().GetHit(playerStats.AttackDamage, playerStats.AttackStun);
                gameObject.SetActive(false);
            }
        }
    }
}