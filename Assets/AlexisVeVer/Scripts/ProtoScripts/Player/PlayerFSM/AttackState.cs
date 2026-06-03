using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            AudioManager.Instance.PlaySound("Whoosh");
            if (playerController.doAttackLeftHand)
            {
                playerController.characterAnimator.SetTrigger("AttackLeft");
            }
            
            if (playerController.doAttackRightHand)
            {
                playerController.characterAnimator.SetTrigger("AttackRight");
            }
            
            playerController.rb.AddForce(playerController.transform.forward * playerController.playerStats.LungeForce);
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.attackOver = false;
            playerController.doAttackLeftHand = false;
            playerController.doAttackRightHand = false;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.attackOver)
            {
                return new IdleState();
            }
            
            return null;
        }
    }
}
