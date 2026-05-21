using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class DeathState : PlayerStateMachine
    {
        public override void OnStateEnter(PlayerController playerController)
        {
            playerController.CharacterAnimator.SetTrigger("Dead");
        }

        public override void OnUpdate(PlayerController playerController)
        {
            
        }

        public override void OnStateExit(PlayerController playerController)
        {
            // There is no exiting death
        }

        public override PlayerStateMachine NextState(PlayerController playerController)
        {
            return null;
        }
    }
}
