using Player;
using UnityEngine;

namespace Items
{
    public class ExplosionDamage : MonoBehaviour
    {
        [SerializeField] private float _hitDamage;
        [SerializeField] private float _hitStun;
        [SerializeField] private float _explosionKnockback;
        
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>().GetHit(_hitDamage, _hitStun, _explosionKnockback, gameObject);
            }
        }
    }
}
