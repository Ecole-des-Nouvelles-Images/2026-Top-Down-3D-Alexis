using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class ItemDamage : MonoBehaviour
    {
        [SerializeField] private float _hitDamage;
        [SerializeField] private float _hitStun;
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.GetComponent<PlayerHealth>().GetHit(_hitDamage, _hitStun);
            }
        }
    }
}
