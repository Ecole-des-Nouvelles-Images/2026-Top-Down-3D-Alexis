using Player;
using UnityEngine;

namespace Items
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PlayerController CurrentHolder;
        
        public abstract void Equip(PlayerController playerController);
        public abstract void Use(PlayerController playerController);
        public abstract void AutoUse(PlayerController playerController);
    }
}