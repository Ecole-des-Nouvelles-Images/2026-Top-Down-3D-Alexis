using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GroundedScript : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float _radius = 0.5f;

        private void Update() {
            RaycastHit[] hits =Physics.SphereCastAll(
                transform.position + offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                _playerController.IsGrounded = false;
                _playerController.gameObject.transform.SetParent(null);
                return;
            }
            
            _playerController.IsGrounded = true;
            _playerController.gameObject.transform.SetParent(hits[0].transform);
            return;
        }
        
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offset, _radius);
        }  
    }
    
    
        //private void OnTriggerStay(Collider other)
        //{
        //    RaycastHit[] hits =Physics.SphereCastAll(transform.position + offset, _radius, Vector3.down);
        //    
        //    if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        //        _playerController.IsGrounded = true;
        //        _playerController.gameObject.transform.SetParent(other.gameObject.transform);
        //    }
        //}
//
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
        //        _playerController.IsGrounded = false;
        //    }
        //}
  //  }
}
