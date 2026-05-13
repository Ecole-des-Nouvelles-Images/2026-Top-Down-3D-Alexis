using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class KatanaAttack : Weapon
    {
        [SerializeField] private GameObject _hitbox;
        
        public override void Equip(PlayerController playerController) { }

        public override void Use(PlayerController playerController)
        {
            _hitbox.SetActive(true);
        }

        public override void AutoUse(PlayerController playerController) { }
    }
}
