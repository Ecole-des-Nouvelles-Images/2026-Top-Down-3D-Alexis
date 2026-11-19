using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace Player.PlayerFSM
{
    public class JumpState : PlayerStateMachine
    {
        private float _movementCheck = 0.2f;
        
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.jumpSound.Play();
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
            playerController.landingSound[Random.Range(0, playerController.landingSound.Count)].Play();
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
