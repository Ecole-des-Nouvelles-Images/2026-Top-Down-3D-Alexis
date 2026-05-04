using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class HandGrabsThing : MonoBehaviour
    {
        [SerializeField] private GameObject _previousParent;
        private GameObject _collidedPlayer;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.transform.parent != null)
                {
                    _previousParent = other.gameObject.transform.parent.gameObject;
                }
                other.gameObject.transform.SetParent(transform);
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_previousParent != null)
                {
                    other.gameObject.transform.parent.SetParent(_previousParent.transform);
                }
                else
                {
                    other.gameObject.transform.SetParent(null);
                }
            }
        }
    }
}