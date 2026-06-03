using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class ExplosionDamage : MonoBehaviour
    {
        [SerializeField] private float _hitDamage;
        [SerializeField] private float _hitStun;
        
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player") && gameObject.transform.parent.transform != collider.gameObject.transform)
            {
                //collider.GetComponent<PlayerHealth>().GetHit(_hitDamage, _hitStun);
            }
        }
    }
}
