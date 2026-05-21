using AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    [CreateAssetMenu(fileName = "PlayerOneStats", menuName = "Scriptable Objects/PlayerOneStats")]
    public class PlayerStats : ScriptableObject
    {
        [Header("Health parameters"), Space(4)]
        public float MaxHealth;
        public float CurrentHealth;
        
        [Header("Stun parameters"), Space(4)]
        public float StaggerForce;
        public float MinStun;
        public float CurrentStun;
        public float MaxStun;
        public float StunDuration;

        [Header("Attack parameters"), Space(4)]
        public float AttackDamage;
        public float AttackStun;
        public float AttackDuration;
        
        [Header("Movements parameters"), Space(4)]
        public float WalkingSpeedModifier;
        public float AirSpeedModifier;
        public float ItemCarrySpeedModifier;
        public float RotationSpeed;
        public float JumpForceModifier;
        public float AdditionalGravity = 10;
        public float MaxSpeed;
        public float SlideForceMultiplier;

        [Header("Slide parameters"), Space(4)] 
        public float SlideCd;

        [Header("Items parameters"), Space(4)] 
        public bool ItemPickedUp;
    }
}
