using System;
using System.Collections;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class LandMineExplosion : MonoBehaviour
    {
        private GameObject _playerInExplosionRadius;
        
        [SerializeField] private LandMine _landMine;

        [Header ("ExplosionPush"), Space(4)]
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadiusModifier;
        [SerializeField] private float _upwardsForceModifier;
        
        [SerializeField] private float _mineDamage;
        [SerializeField] private float _mineStun;

        private void Explosion()
        {
            // Infliger des dégats aux joueurs dans la zone d'explosion
            _playerInExplosionRadius.GetComponent<PlayerHealth>().GetHit(_mineDamage, _mineStun);
            
            // repousser les joueurs dans la zone d'explosion
            _playerInExplosionRadius.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, gameObject.transform.position, 
                    _explosionRadiusModifier, _upwardsForceModifier);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player in radius");
                _playerInExplosionRadius = other.gameObject;
                Explosion();
            }
        }
    }
}
