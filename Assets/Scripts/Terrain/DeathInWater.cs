using Player;
using UnityEngine;

namespace Terrain
{
    public class DeathInWater : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().isDrowned = true;
            }
        }
    }
}