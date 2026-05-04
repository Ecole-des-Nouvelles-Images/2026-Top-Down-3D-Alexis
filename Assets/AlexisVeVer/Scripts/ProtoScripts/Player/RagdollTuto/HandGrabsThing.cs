using Unity.VisualScripting;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class HandGrabsThing : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.SetParent(transform);
            }
        }
        
    }
}
