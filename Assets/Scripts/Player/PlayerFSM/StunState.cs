using UnityEngine;

namespace Player.PlayerFSM
{
    public class StunState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            Debug.Log("Stun State Entry");
            playerController.characterAnimator.SetBool("IsStunned", true);
            playerController.canJump = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            Debug.Log("Stun State Exit");
            playerController.characterAnimator.SetBool("IsStunned", false);
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
