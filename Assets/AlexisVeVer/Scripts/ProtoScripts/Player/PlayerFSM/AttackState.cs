using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            if (playerController.doAttackLeftHand)
            {
                playerController.CharacterAnimator.SetTrigger("AttackRight");
            }
            
            if (playerController.doAttackRightHand)
            {
                playerController.CharacterAnimator.SetTrigger("AttackLeft");
            }
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.attackOver = false;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.attackOver)
            {
                return new IdleState();
            }
            
            if (playerController.playerHealth.IsStunned)
            {
                return new StunnedState();
            }
            
            if (playerController.playerHealth.IsDead)
            {
                return new DeathState();
            }
            
            return null;
        }
    }
}
