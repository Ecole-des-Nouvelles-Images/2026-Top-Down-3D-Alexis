using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM;
using AlexisVeVer.Scripts.ProtoScripts.Terrain;
using UnityEngine;
using UnityEngine.Serialization;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class GroundedScript : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _radius = 0.5f;

        private void Update() {
            RaycastHit[] hits =Physics.SphereCastAll(
                transform.position + _offset, _radius, Vector3.down,  0,_groundLayer);

            if (hits.Length <= 0) {
                playerController.IsGrounded = false;
                playerController.canJump = false;
                playerController.characterAnimator.SetBool("IsGrounded", false);
                playerController.gameObject.transform.SetParent(null);
                playerController.IsTerrainMoving = false;
            }

            else
            {
                playerController.IsGrounded = true;
                playerController.canJump = true;
                playerController.characterAnimator.SetBool("IsGrounded", true);
                playerController.gameObject.transform.SetParent(hits[0].transform);
                if (hits[0].transform.gameObject.GetComponent<TerrainFloatAway>()?.FloatsAway == true)
                {
                    playerController.IsTerrainMoving = true;
                }
            }
        }
        
        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _offset, _radius);
        }  
    }
}