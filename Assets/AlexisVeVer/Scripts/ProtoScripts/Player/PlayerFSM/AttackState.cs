using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            //fsmControllerSetup.Animator.SetBool("Hit", true);
            playerController.canAttack = false;
            playerController.canSlide = false;
            playerController.canJump = false;
        }

        public override void OnUpdate(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            throw new System.NotImplementedException();
        }
    }
}
