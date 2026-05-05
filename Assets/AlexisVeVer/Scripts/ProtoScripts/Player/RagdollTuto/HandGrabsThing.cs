using System;
using Unity.VisualScripting;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto
{
    public class HandGrabsThing : MonoBehaviour
    {
        private GameObject _previousParent;
        private GameObject _collidedPlayer;
        
        //Collided object references
        private SpringJoint _jointTouched;
        private Rigidbody _jointTargetRb;
        
        //GO's RigidBody
        [Header("Rigidbody target will follow")]
        [SerializeField] private Rigidbody _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //Stocke le parent du gameObjet touché si c'est un joueur
                // if (other.gameObject.transform.parent != null)
                // {
                //     _previousParent = other.gameObject.transform.parent.gameObject;
                // }

                // Permet de changer le parent du game object touché par celui qui l'attrape
                // other.gameObject.transform.SetParent(transform);
                
                //Récupère le configurableJoint du gameObject attrapé
                if (other.gameObject.GetComponent<SpringJoint>() == true)
                {
                    _jointTouched = other.gameObject.GetComponent<SpringJoint>();
                    Debug.Log("On a un configurable joint " + _jointTouched);
                    
                    //Récupère le rigidBody auquel le gameObject est connecté
                    if (_jointTouched.connectedBody != null)
                    {
                        _jointTargetRb = _jointTouched.connectedBody;
                        Debug.Log("On a un rigidbody");
                    }
                    else
                    {
                        Debug.Log("Le connectedBody avait pas de cible");
                    }
                }
                
                //Assigne le rigidbody de l'objet qui attrape en temps que connexion du rigidbody de l'objet attrapé
                _jointTouched.connectedBody = _rb;
                Debug.Log("On a donné notre rb à ", other.gameObject);
            }
        }

        void OnTriggerStay(Collider other)
        {
            //Permet de garder le gameobject qui touche l'autre en temps que son parent
            // if (other.gameObject.CompareTag("Player"))
            // {
            //     other.gameObject.transform.SetParent(transform);
            // }
        }

        private void OnTriggerExit(Collider other)
        {
            // Redonne le parent d'origine à l'objet attrapé quand il est lâché
            // if (other.gameObject.CompareTag("Player"))
            // {
            //     if (_previousParent != null)
            //     {
            //         other.gameObject.transform.parent.SetParent(_previousParent.transform);
            //     }
            //     else
            //     {
            //         other.gameObject.transform.SetParent(null);
            //     }
            // }
            
            //Rends le rigidBody suivi d'origine au configurableJoint de l'objet lâché
            if (_jointTargetRb != null || _jointTargetRb != _rb)
            {
                _jointTouched.connectedBody = _jointTargetRb;
            }
            else
            {
                _jointTouched.connectedBody = null;
            }
            Debug.Log("On a rendu le bousin");
        }
    }
}