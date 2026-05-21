using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class StunnedState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.CharacterAnimator.SetTrigger("Stun");
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.playerHealth.CurrentStun = playerController.PlayerStats.MinStun;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            
            if (!playerController.playerHealth.IsStunned)
            {
                return new IdleState();
            }
            
            return null;
        }
    }
}
