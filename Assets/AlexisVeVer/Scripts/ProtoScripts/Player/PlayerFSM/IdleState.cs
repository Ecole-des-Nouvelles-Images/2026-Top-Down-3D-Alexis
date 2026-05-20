using System;
using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class IdleState : PlayerStateMachine
    {
        public override void OnStateEnter(FsmControllerSetup fsmControllerSetup)
        {
            //fsmControllerSetup.Animator.SetBool("isIdle", true);
            fsmControllerSetup.canAttack = true;
            fsmControllerSetup.canSlide = false;
            fsmControllerSetup.canJump = true;
        }

        public override void OnUpdate(FsmControllerSetup fsmControllerSetup)
        {
            fsmControllerSetup.Move(fsmControllerSetup.PlayerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(FsmControllerSetup fsmControllerSetup)
        {
            //fsmControllerSetup.Animator.SetBool("isIdle", false);
        }

        public override PlayerStateMachine NextState(FsmControllerSetup fsmControllerSetup)
        {
            if (fsmControllerSetup.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }

            if (!fsmControllerSetup.IsGrounded && fsmControllerSetup.canJump)
            {
                return new JumpState();
            }
            
            return null;
        }
    }
}