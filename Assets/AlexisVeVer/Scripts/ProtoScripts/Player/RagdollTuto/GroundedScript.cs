using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GroundedScript : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private FsmControllerSetup _fsmControllerSetup;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float _radius = 0.5f;

        private void Update() {
            RaycastHit[] hits =Physics.SphereCastAll(
                transform.position + offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                if (_playerController != null)
                {
                    _playerController.IsGrounded = false;
                    _playerController.gameObject.transform.SetParent(null);
                    return;
                }
                
                if (_fsmControllerSetup != null)
                {
                    _fsmControllerSetup.IsGrounded = false;
                    _fsmControllerSetup.gameObject.transform.SetParent(null);
                    return;
                }
            }
            
            if (_playerController != null)
            {
                _playerController.IsGrounded = true;
                _playerController.gameObject.transform.SetParent(hits[0].transform);
            }
            
            if (_fsmControllerSetup != null)
            {
                _fsmControllerSetup.IsGrounded = true;
                _fsmControllerSetup.gameObject.transform.SetParent(hits[0].transform);
                return;
            }
        }
        
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + offset, _radius);
        }  
    }
}
