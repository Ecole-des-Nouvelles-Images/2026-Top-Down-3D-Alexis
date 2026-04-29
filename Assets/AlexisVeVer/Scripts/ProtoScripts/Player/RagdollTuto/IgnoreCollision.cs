using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class IgnoreCollision : MonoBehaviour
    {
        [SerializeField] private Collider _colliderThatIgnoresCollision;

        [SerializeField] private Collider[] _collidersToIgnore;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            foreach (Collider col in _collidersToIgnore)
            {
                Physics.IgnoreCollision(_colliderThatIgnoresCollision, col, true);
            }
        }
    }
}
