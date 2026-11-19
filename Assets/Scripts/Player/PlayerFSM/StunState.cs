using UnityEngine;

namespace Player.PlayerFSM
{
    public class StunState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.characterAnimator.SetBool("IsStunned", true);
            playerController.canJump = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.characterAnimator.SetBool("IsStunned", false);
            playerController.canJump = true;
            Debug.Log("exit stun state");
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (!playerController.isStunned)
            {
                return new IdleState();
            }
            
            return null;
        }
    }
}
