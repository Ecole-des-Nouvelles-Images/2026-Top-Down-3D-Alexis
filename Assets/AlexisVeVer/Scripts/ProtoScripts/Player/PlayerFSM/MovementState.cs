using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class MovementState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.CharacterAnimator.SetBool("Walking", true);
            playerController.canJump = true;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.PlayerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(PlayerController playerController)
        { 
            playerController.CharacterAnimator.SetBool("Walking", false);
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
            
            if (playerController.IsGrounded && playerController.doAttackLeftHand || playerController.doAttackRightHand)
            {
                return new AttackState();
            }
            
            if (playerController.playerHealth.GotHit && !playerController.playerHealth.IsStunned && !playerController.playerHealth.IsDead)
            {
                return new StaggerState();
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