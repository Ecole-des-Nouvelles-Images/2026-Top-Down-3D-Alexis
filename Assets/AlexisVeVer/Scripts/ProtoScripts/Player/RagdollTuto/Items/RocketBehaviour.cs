using System;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Player.RagdollTuto.Items
{
    public class RocketBehaviour : MonoBehaviour
    {
        [SerializeField] private float _gravityForce;
        [SerializeField] private float _directHitDamage;
        [SerializeField] private float _directHitStun;
        
        [SerializeField] private GameObject _explosionPrefab;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _rb.AddForce(Vector3.down * _gravityForce);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Bazooka")) return;
            Instantiate(_explosionPrefab);
            Destroy(gameObject);

            if (collider.gameObject.CompareTag("Player"))
            {
                // collider.GetComponent<PlayerHealth>().GetHit(_directHitDamage, _directHitStun);
            }
        }
    }
}
