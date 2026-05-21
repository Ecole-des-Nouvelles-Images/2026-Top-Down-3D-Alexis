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
            playerController.Move(playerController.PlayerStats.ItemCarrySpeedModifier);
            if (playerController.doAttackRightHand || playerController.doAttackLeftHand)
            {
                playerController.CurrentWeapon.Use(playerController);
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
            if (playerController.CurrentWeapon == null)
            {
                return new IdleState();
            }
            
            if (playerController.CurrentWeapon != null && playerController.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            
            if (playerController.CurrentWeapon != null && !playerController.IsGrounded)
            {
                return new JumpState();
            }
            
            if (playerController.CurrentWeapon != null && playerController.canSlide && playerController.doSlide)
            {
                return new SlideState();
            }
            
            return null;
        }
    }
}