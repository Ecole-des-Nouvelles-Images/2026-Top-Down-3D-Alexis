using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public abstract class PlayerStateMachine
    {
        public abstract void OnStateEnter(FsmControllerSetup playerController);
        
        public abstract void OnUpdate(FsmControllerSetup playerController);
        
        public abstract void OnStateExit(FsmControllerSetup playerController);
        
        public abstract PlayerStateMachine NextState(FsmControllerSetup playerController);
    }
}