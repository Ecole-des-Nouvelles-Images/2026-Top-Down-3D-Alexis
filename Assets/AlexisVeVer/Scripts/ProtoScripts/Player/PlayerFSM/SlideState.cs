using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class SlideState : PlayerStateMachine
    {
        public override void OnStateEnter(FsmControllerSetup fsmControllerSetup)
        {
            Debug.Log("TimeInSlideState");
            //fsmControllerSetup.Animator.SetBool("Slide", true);
            fsmControllerSetup.canAttack = true;
            fsmControllerSetup.canJump = false;
            
            fsmControllerSetup.Rb.AddForce(
                new Vector3(fsmControllerSetup.Rb.linearVelocity.x * fsmControllerSetup.PlayerStats.SlideForceMultiplier, 0,
                    fsmControllerSetup.Rb.linearVelocity.z * fsmControllerSetup.PlayerStats.SlideForceMultiplier),
                ForceMode.VelocityChange);
            fsmControllerSetup.canSlide = false;
        }

        public override void OnUpdate(FsmControllerSetup fsmControllerSetup)
        {
            
        }

        public override void OnStateExit(FsmControllerSetup fsmControllerSetup)
        {
            
        }

        public override PlayerStateMachine NextState(FsmControllerSetup fsmControllerSetup)
        {
            if (fsmControllerSetup.IsGrounded && fsmControllerSetup.Rb.linearVelocity.magnitude <= 0.3f)
            {
                return new MovementState();
            }
            
            if (!fsmControllerSetup.IsGrounded)
            {
                return new JumpState();
            }
            
            return null;
        }
    }
}
