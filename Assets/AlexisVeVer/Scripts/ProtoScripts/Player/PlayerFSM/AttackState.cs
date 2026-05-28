using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            if (playerController.doAttackLeftHand)
            {
                playerController.characterAnimator.SetTrigger("AttackLeft");
                playerController.attackHitboxLeftHand.SetActive(true);
            }
            
            if (playerController.doAttackRightHand)
            {
                playerController.characterAnimator.SetTrigger("AttackRight");
                playerController.attackHitboxRightHand.SetActive(true);
            }
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            playerController.attackOver = false;
            playerController.doAttackLeftHand = false;
            playerController.doAttackRightHand = false;
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
