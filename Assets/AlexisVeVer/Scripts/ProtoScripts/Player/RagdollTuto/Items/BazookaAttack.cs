using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class BazookaAttack : Weapon
    {
        public override void Equip(PlayerController playerController) { }

        public override void Use(PlayerController playerController)
        {
            Debug.Log("FireRocket");
            Destroy(gameObject);
        }

        public override void AutoUse(PlayerController playerController) { }
    }
}
