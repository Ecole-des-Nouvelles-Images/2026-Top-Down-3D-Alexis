using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class StunState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //playerController.Animator.SetBool("Stun", true);
            playerController.canAttack = false;
            playerController.canSlide = false;
            playerController.canJump = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }
    }
}
