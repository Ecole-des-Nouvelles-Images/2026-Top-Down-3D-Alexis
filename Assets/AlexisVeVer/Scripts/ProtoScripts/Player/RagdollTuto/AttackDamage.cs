using UnityEngine;
using UnityEngine.Serialization;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class AttackDamage : MonoBehaviour
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private GameObject _hitParticle;

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
                if (other.GetComponent<PlayerController>() == GetComponentInParent<PlayerController>()) return;
                
                other.GetComponent<PlayerHealth>().GetHit(_playerStats.AttackDamage, _playerStats.AttackStun, _playerStats.KnockBackForce);
                Instantiate(_hitParticle, other.transform.position, Quaternion.identity);
            }
        }
    }
}