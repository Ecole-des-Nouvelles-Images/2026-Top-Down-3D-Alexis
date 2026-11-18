using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player
{
    public class SyncPlayerVisual : MonoBehaviour
    {
        [Header("Bone001 ConfigurableJoint")]
        public ConfigurableJoint visualParentJoint;
        
        [Header("PlayerController"), Space(5)]
        public GameObject handSocket;
        public Animator characterAnimator;
        public SyncPhysicsObject[] syncPhysicsObjects;

        [Header("CollidersToIgnore"), Space(5)]
        public Collider[] collidersToIgnore;
        
        [Header("PlayerHealth"), Space(5)]
        public Renderer skinnedMeshHolder;

        [Header("AnimationEvent"), Space(5)] 
        public PlayerController playerController;

        [Header("Sprite"), Space(5)] 
        public Sprite sprite;
        
        [ContextMenu("GetSyncPhysicObjects")]
        public void GetSyncPhysicObjects()
        {
            syncPhysicsObjects = GetComponentsInChildren<SyncPhysicsObject>();
        }
        
        [ContextMenu("GetCollidersToIgnore")]
        public void GetCollidersToIgnore()
        {
            collidersToIgnore = GetComponentsInChildren<Collider>();
        }
    }
}