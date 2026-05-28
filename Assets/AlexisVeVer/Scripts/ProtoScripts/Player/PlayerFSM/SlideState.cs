using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class SlideState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //playerController.Animator.SetBool("Slide", true);
            playerController.canAttack = true;
            playerController.canJump = false;
            
            playerController.Rb.AddForce(
                new Vector3(playerController.Rb.linearVelocity.x * playerController.PlayerStats.SlideForceMultiplier, 0,
                    playerController.Rb.linearVelocity.z * playerController.PlayerStats.SlideForceMultiplier),
                ForceMode.VelocityChange);
            playerController.canSlide = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.doSlide = false;
            Debug.Log("Exiting slide state");
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.IsGrounded && playerController.Rb.linearVelocity.magnitude <= 0.3f)
            {
                return new MovementState();
            }
            
            if (!playerController.IsGrounded)
            {
                return new JumpState();
            }
            
            if (playerController.CurrentWeapon != null && playerController.IsGrounded && playerController.Rb.linearVelocity.magnitude <= 0.3f)
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
