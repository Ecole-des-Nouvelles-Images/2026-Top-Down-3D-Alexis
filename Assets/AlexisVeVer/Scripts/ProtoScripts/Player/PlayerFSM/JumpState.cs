using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.canAttack = false;
            playerController.canJump = false;
            playerController.Rb.linearVelocity = Vector3.zero;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            Debug.Log("Current state is jump state");
            playerController.Move(playerController.PlayerStats.AirSpeedModifier);
            playerController.GravityModification(playerController.PlayerStats.AdditionalGravity);
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.jumped = false;
            Debug.Log("Jump state exit, new state is " + playerController.CurrentState);
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (!playerController.canJump && playerController.Rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (!playerController.canJump && playerController.Rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            
            if (playerController.CurrentWeapon != null && playerController.IsGrounded)
            {
                return new ItemCarryState();
            }
            
            if (playerController.isStunned)
            {
                return new StunState();
            }
            
            return null;
        }
    }
}
