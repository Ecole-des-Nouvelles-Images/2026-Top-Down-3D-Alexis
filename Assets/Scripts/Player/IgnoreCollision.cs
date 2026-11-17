using UnityEngine;

namespace Player
{
    public class IgnoreCollision : MonoBehaviour
    {
        [SerializeField] private Collider _colliderThatIgnoresCollision;

        [SerializeField] private Collider[] _collidersToIgnore;
        [SerializeField] private PlayerController playerController;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (_collidersToIgnore == null) {
                _collidersToIgnore = playerController.PlayerVisual.collidersToIgnore;
                foreach (Collider col in _collidersToIgnore) {
                    Physics.IgnoreCollision(_colliderThatIgnoresCollision, col, true);
                }
            }
            else {
                foreach (Collider col in _collidersToIgnore) {
                    Physics.IgnoreCollision(_colliderThatIgnoresCollision, col, true);
                }
                
            }
        }
    }
}
