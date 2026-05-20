using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public class AttackState : PlayerStateMachine
    {
        public override void OnStateEnter(FsmControllerSetup fsmControllerSetup)
        {
            //fsmControllerSetup.Animator.SetBool("Hit", true);
            fsmControllerSetup.canAttack = false;
            fsmControllerSetup.canSlide = false;
            fsmControllerSetup.canJump = false;
        }

        public override void OnUpdate(FsmControllerSetup fsmControllerSetup)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit(FsmControllerSetup fsmControllerSetup)
        {
            throw new System.NotImplementedException();
        }

        public override PlayerStateMachine NextState(FsmControllerSetup fsmControllerSetup)
        {
            throw new System.NotImplementedException();
        }
    }
}
