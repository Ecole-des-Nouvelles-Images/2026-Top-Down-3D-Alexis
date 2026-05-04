using UnityEngine;
using UnityEngine.InputSystem;

namespace AlexisVeVer.Scripts.ProtoScripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;
        
        private Vector2 _move;
        
        [SerializeField] private float _speedMultiplier;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //_rb.linearVelocity = new Vector3(_move.x, 0, _move.y);
            _rb.linearVelocity = new Vector3(_move.x * _speedMultiplier, 0, _move.y * _speedMultiplier);
        }
        
        private void OnMove(InputValue valeur)
        {
            _move = valeur.Get<Vector2>();
        }
    }
}
