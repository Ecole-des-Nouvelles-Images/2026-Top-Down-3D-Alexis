using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class FanPropelsAir : Weapon
    {
        public override void Equip(PlayerController playerController)
        {
            Invoke(nameof(DestroyMe), 3f);
        }

        public override void Use(PlayerController playerController) { }

        public override void AutoUse(PlayerController playerController)
        {
            Debug.Log("FanVentilates");
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
