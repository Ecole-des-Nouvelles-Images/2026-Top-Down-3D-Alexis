using UnityEngine;

namespace Player
{
    public class FootstepSpawner : MonoBehaviour
    {
        [Header("References")]
        public GameObject footprintPrefab;
        public Animator footprintAnimator;

        public Transform leftFootPoint;
        public Transform rightFootPoint;

        [Header("Settings")]
        public LayerMask groundMask;

        public float rayDistance = 1f;
        public float footprintOffset = 0.01f;
        public float footprintLifetime = 10f;
        
        public void LeftStep()
        {
            SpawnFootprint(leftFootPoint);
        }

        public void RightStep()
        {
            SpawnFootprint(rightFootPoint);
        }

        void SpawnFootprint(Transform foot)
        {
            RaycastHit hit;

            Vector3 origin = foot.position + Vector3.up * 0.2f;

            if (Physics.Raycast(origin, Vector3.down, out hit, rayDistance, groundMask))
            {
                Quaternion rotation = Quaternion.LookRotation(transform.forward, hit.normal);

                GameObject footprint = Instantiate(footprintPrefab, hit.point + hit.normal * footprintOffset, rotation);

                Destroy(footprint, footprintLifetime);
            }
        }
    }
}
