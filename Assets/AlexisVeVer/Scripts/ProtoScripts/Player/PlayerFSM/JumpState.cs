using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        public override void OnStateEnter(FsmControllerSetup fsmControllerSetup)
        {
            Debug.Log("JumpStateEnter");
            //fsmControllerSetup.Animator.SetBool("Jump", true);
            fsmControllerSetup.canAttack = false;
            fsmControllerSetup.canSlide = true;
            fsmControllerSetup.canJump = false;
        }

        public override void OnUpdate(FsmControllerSetup fsmControllerSetup)
        {
            fsmControllerSetup.Move(fsmControllerSetup.PlayerStats.AirSpeedModifier);
            fsmControllerSetup.GravityModification(fsmControllerSetup.PlayerStats.AdditionalGravity);
        }

        public override void OnStateExit(FsmControllerSetup fsmControllerSetup)
        {
            //fsmControllerSetup.Animator.SetBool("Jump", false);
        }

        public override PlayerStateMachine NextState(FsmControllerSetup fsmControllerSetup)
        {
            if (fsmControllerSetup.IsGrounded && fsmControllerSetup.Rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (fsmControllerSetup.IsGrounded && fsmControllerSetup.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            return null;
        }
    }
}
