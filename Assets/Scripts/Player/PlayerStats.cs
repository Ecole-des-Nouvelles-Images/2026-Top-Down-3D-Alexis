using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerOneStats", menuName = "Scriptable Objects/PlayerOneStats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Health parameters"), Space(4)]
        public float MaxHealth;
        
        [Header("Stun parameters"), Space(4)]
        public float MinStun;
        public float MaxStun;

        [Header("Attack parameters"), Space(4)]
        public float AttackDamage;
        public float AttackStun;
        public float AttackDuration;
        public float LungeForce;
        public float KnockBackForce;
        
        [Header("Movements parameters"), Space(4)]
        public float WalkingSpeedModifier;
        public float AirSpeedModifier;
        public float ItemCarrySpeedModifier;
        public float FloatingTerrainSpeedModifier;
        public float RotationSpeed;
        public float JumpForceModifier;
        public float AdditionalGravity = 10;
        public float MaxSpeed = 3;
        public float SlideForceMultiplier;

        [Header("Slide parameters"), Space(4)] 
        public float SlideCd;

        [Header("Items parameters"), Space(4)] 
        public bool ItemPickedUp;
    }
}
