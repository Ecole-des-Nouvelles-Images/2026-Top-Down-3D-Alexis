using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GrabHandler : MonoBehaviour
    {
        [SerializeField] Animator _animator;

        //Fixed joint created on the fly
        private FixedJoint _fixedJoint;

        private Rigidbody _rb;
        
        //Reference
        private PlayerController _playerController;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerController = GetComponent<PlayerController>();
        }
    }
}
