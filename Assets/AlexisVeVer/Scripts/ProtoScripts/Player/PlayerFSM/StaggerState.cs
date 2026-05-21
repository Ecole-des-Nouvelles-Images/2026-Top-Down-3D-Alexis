using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class StaggerState : PlayerStateMachine
    {
        private float _staggerTime;
        
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.Rb.AddForce( - playerController.transform.forward * playerController.PlayerStats.StaggerForce, ForceMode.Impulse);
        }

        public override void OnUpdate(PlayerController playerController)
        {
            _staggerTime += Time.deltaTime;
        }

        public override void OnStateExit(PlayerController playerController)
        {
            _staggerTime = 0;
            playerController.playerHealth.GotHit = false;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.Rb.linearVelocity.magnitude <= 0.2f && _staggerTime >= 0.2f)
            {
                return new IdleState();
            }
            
            return null;
        }
    }
}