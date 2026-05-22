using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class IdleState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //fsmControllerSetup.Animator.SetBool("isIdle", true);
            playerController.canAttack = true;
            playerController.canJump = true;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.PlayerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(PlayerController playerController)
        {
            //fsmControllerSetup.Animator.SetBool("isIdle", false);
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }

            if (!playerController.IsGrounded && !playerController.jumped)
            {
                return new JumpState();
            }

            if (playerController.CurrentWeapon != null)
            {
                return new ItemCarryState();
            }

            if (playerController.IsGrounded && playerController.doAttackLeftHand || playerController.doAttackRightHand)
            {
                return new AttackState();
            }
            
            if (playerController.isStunned)
            {
                return new StunState();
            }
            
            return null;
        }
    }
}