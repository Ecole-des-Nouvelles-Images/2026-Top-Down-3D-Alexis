using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        private float _movementCheck = 0.2f;
        
        public override void OnStateEnter(PlayerController playerController)
        {
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
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (!playerController.canJump && playerController.moveInput.magnitude <= _movementCheck)
            {
                return new IdleState();
            }
            
            if (!playerController.canJump && playerController.moveInput.magnitude >= _movementCheck)
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
