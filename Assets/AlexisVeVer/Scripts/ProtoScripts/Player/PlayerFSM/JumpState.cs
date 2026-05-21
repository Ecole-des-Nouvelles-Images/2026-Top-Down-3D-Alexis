using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //playerController.Animator.SetBool("Jump", true);
            playerController.canAttack = false;
            playerController.canJump = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.PlayerStats.AirSpeedModifier);
            playerController.GravityModification(playerController.PlayerStats.AdditionalGravity);
        }

        public override void OnStateExit(PlayerController playerController)
        {
            //playerController.Animator.SetBool("Jump", false);
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.IsGrounded && playerController.Rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (playerController.IsGrounded && playerController.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            
            if (playerController.CurrentWeapon != null && playerController.IsGrounded)
            {
                return new ItemCarryState();
            }
            
            return null;
        }
    }
}
