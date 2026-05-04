using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class SyncPhysicsObject : MonoBehaviour
    {
        private Rigidbody _rb ;
        private ConfigurableJoint _joint;

        [SerializeField] private Rigidbody _animatedRb;

        [SerializeField] private bool _syncAnimation;
        
        //Keep track of rotation
        private Quaternion _startLocalRotation;


        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _joint = GetComponent<ConfigurableJoint>();
            
            _startLocalRotation = transform.localRotation;
        }

        public void UpdateJointFromAnimation()
        {
            if (!_syncAnimation)
            {
                return;
            }
            
            ConfigurableJointExtensions.SetTargetRotationLocal(_joint, _animatedRb.transform.localRotation, _startLocalRotation);
        }
    }
}
