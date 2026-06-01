using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            Debug.Log("Jump state enter");
            AudioManager.Instance.PlaySound("Whoosh");
            AudioManager.Instance.PlaySound("Step3");
            playerController.canAttack = false;
            playerController.rb.linearVelocity = Vector3.zero;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.playerStats.AirSpeedModifier);
            playerController.GravityModification(playerController.playerStats.AdditionalGravity);
        }

        public override void OnStateExit(PlayerController playerController)
        {
            AudioManager.Instance.PlaySound("Ground");
            playerController.jumped = false;
            Debug.Log("Jump state exit, new state is " + playerController.CurrentState);
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (!playerController.canJump && playerController.rb.linearVelocity.magnitude <= 0.2f)
            {
                return new IdleState();
            }
            
            if (!playerController.canJump && playerController.rb.linearVelocity.magnitude >= 0.2f)
            {
                return new MovementState();
            }
            
            if (playerController.currentWeapon != null && playerController.IsGrounded)
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
