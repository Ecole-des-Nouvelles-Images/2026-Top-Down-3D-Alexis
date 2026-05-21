using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class MovementState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //playerController.Animator.SetBool("Movement", true);
            playerController.canAttack = true;
            playerController.canJump = true;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.PlayerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(PlayerController playerController)
        { 
            Debug.Log("Movement State::OnStateExit");
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.Rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (!playerController.IsGrounded && playerController.canJump)
            {
                return new JumpState();
            }
            
            if (playerController.canSlide && playerController.doSlide)
            {
                return new SlideState();
            }
            
            if (playerController.CurrentWeapon != null)
            {
                return new ItemCarryState();
            }
            
            return null;
        }
    }
}