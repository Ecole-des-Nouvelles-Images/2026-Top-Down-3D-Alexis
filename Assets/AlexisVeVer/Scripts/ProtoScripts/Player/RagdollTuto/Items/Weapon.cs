using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Equip(PlayerController playerController);
        public abstract void Use(PlayerController playerController);
        public abstract void AutoUse(PlayerController playerController);
    }
}