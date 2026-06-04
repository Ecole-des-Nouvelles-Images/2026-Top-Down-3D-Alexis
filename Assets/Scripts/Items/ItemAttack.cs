using System;
using UnityEngine;

namespace Items
{
    public class ItemAttack : MonoBehaviour
    {
        public event EventHandler<Collider> onHit;
        
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                onHit?.Invoke(this, collider);
            }
        }
    }
}
