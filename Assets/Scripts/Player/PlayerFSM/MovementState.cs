namespace Player.PlayerFSM
{
    public class MovementState : PlayerStateMachine
    {
        private float _movementCheck = 0.2f;
        
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.characterAnimator.SetBool("Walking", true);
            playerController.canAttack = true;
            playerController.canJump = true;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            playerController.Move(playerController.playerStats.WalkingSpeedModifier);
        }

        public override void OnStateExit(PlayerController playerController)
        { 
            playerController.characterAnimator.SetBool("Walking", false);
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.moveInput.magnitude <= _movementCheck)
            {
                return new IdleState();
            }
            
            if (!playerController.IsGrounded && playerController.canJump)
            {
                return new JumpState();
            }
            
            if (playerController.canSlide && playerController.doSlide)
            {
                return new SlideState();
            }
            
            if (playerController.currentWeapon != null)
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