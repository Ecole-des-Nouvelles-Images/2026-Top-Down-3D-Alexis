using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            if (playerController.doAttackLeftHand)
            {
                playerController.CharacterAnimator.SetTrigger("AttackLeft");
            }
            
            if (playerController.doAttackRightHand)
            {
                playerController.CharacterAnimator.SetTrigger("AttackRight");
            }
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.attackOver = false;
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            if (playerController.attackOver)
            {
                return new IdleState();
            }
            
            return null;
        }
    }
}
