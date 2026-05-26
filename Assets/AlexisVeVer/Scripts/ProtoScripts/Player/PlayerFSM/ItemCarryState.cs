using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class ItemCarryState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            Debug.Log("Entered ItemCarryState");
            
            //playerController.Animator.SetBool("CarryItem", true);
            playerController.canAttack = false;
            playerController.canJump = true;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.playerStats.ItemCarrySpeedModifier);
            if (playerController.doAttackRightHand || playerController.doAttackLeftHand)
            {
                playerController.currentWeapon.Use(playerController);
                playerController.doAttackRightHand = false;
                playerController.doAttackLeftHand  = false;
            }
        }

        public override void OnStateExit(PlayerController playerController)
        {
            Debug.Log("Exited ItemCarryState");
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.currentWeapon == null)
            {
                return new IdleState();
            }
            
            if (playerController.currentWeapon != null && playerController.rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            
            if (playerController.currentWeapon != null && !playerController.IsGrounded)
            {
                return new JumpState();
            }
            
            if (playerController.currentWeapon != null && playerController.canSlide && playerController.doSlide)
            {
                return new SlideState();
            }
            
            if (playerController.isStunned)
            {
                return new StunState();
            }
            
            return null;
        }
    }
}