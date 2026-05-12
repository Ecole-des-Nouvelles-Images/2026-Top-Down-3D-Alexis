using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class DropMine : Weapon
    {
        public override void Equip(PlayerController playerController) { }

        public override void Use(PlayerController playerController)
        {
            Debug.Log("Mine");
            Destroy(gameObject);
        }

        public override void AutoUse(PlayerController playerController) { }
        
    }
}
