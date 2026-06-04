using UnityEngine;

namespace Player.PlayerFSM
{
    public class SlideState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            Debug.Log("Entered Slide State");
            playerController.characterAnimator.SetBool("IsSliding", true);
            playerController.canAttack = true;
            playerController.canJump = false;
            
            playerController.rb.AddForce(
                new Vector3(playerController.rb.linearVelocity.x * playerController.playerStats.SlideForceMultiplier, 0,
                    playerController.rb.linearVelocity.z * playerController.playerStats.SlideForceMultiplier),
                ForceMode.VelocityChange);
            playerController.canSlide = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.doSlide = false;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.IsGrounded && playerController.rb.linearVelocity.magnitude <= 0.3f)
            {
                return new MovementState();
            }
            
            if (!playerController.IsGrounded)
            {
                return new JumpState();
            }
            
            if (playerController.currentWeapon != null && playerController.IsGrounded && playerController.rb.linearVelocity.magnitude <= 0.3f)
            {
                return new ItemCarryState();
            }
            
            if (playerController.isStunned)
            {
                return new StunState();
            }
            
            return null;
        }
    }
}
