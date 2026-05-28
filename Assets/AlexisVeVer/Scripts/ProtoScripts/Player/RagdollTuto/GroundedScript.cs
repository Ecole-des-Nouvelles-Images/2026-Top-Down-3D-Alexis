using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GroundedScript : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius = 0.5f;

        private void Update() {
            RaycastHit[] hits =Physics.SphereCastAll(
                transform.position + _offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                _playerController.IsGrounded = false;
                _playerController.CharacterAnimator.SetBool("IsGrounded", false);
                _playerController.gameObject.transform.SetParent(null);
            }

            else
            {
                _playerController.IsGrounded = true;
                _playerController.CharacterAnimator.SetBool("IsGrounded", true);
                _playerController.gameObject.transform.SetParent(hits[0].transform);
            }
        }
        
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _offset, _radius);
        }  
    }
}
