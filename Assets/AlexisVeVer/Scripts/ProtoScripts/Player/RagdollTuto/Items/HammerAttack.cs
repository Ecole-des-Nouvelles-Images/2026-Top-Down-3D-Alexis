using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class HammerAttack : Weapon
    {
        public override void Equip(PlayerController playerController) { }

        public override void Use(PlayerController playerController)
        {
            Debug.Log("Hammer");
            Destroy(gameObject);
        }

        public override void AutoUse(PlayerController playerController) {}
    }
}
