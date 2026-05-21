using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.PlayerFSM
{
    public abstract class PlayerStateMachine
    {
        public abstract void OnStateEnter(PlayerController playerController);
        
        public abstract void OnUpdate(PlayerController playerController);
        
        public abstract void OnStateExit(PlayerController playerController);
        
        public abstract PlayerStateMachine NextState(PlayerController playerController);
    }
}