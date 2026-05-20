using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class MovementState : PlayerStateMachine
    {
        public override void OnStateEnter(FsmControllerSetup fsmControllerSetup)
        {
            //fsmControllerSetup.Animator.SetBool("Movement", true);
            fsmControllerSetup.canAttack = true;
            fsmControllerSetup.canSlide = true;
            fsmControllerSetup.canJump = true;
        }

        public override void OnUpdate(FsmControllerSetup fsmControllerSetup)
        {
            fsmControllerSetup.Move(fsmControllerSetup.PlayerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(FsmControllerSetup fsmControllerSetup)
        { 
            
        }

        public override PlayerStateMachine NextState(FsmControllerSetup fsmControllerSetup)
        {
            if (fsmControllerSetup.Rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (!fsmControllerSetup.IsGrounded && fsmControllerSetup.canJump)
            {
                return new JumpState();
            }
            
            if (fsmControllerSetup.canSlide && fsmControllerSetup.doSlide)
            {
                return new SlideState();
            }
            
            return null;
        }
    }
}