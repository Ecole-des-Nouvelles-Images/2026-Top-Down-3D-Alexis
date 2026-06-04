using UnityEngine;

namespace Items
{
    public class RocketBehaviour : MonoBehaviour
    {
        [SerializeField] private float _gravityForce;
        [SerializeField] private GameObject _explosionPrefab;
        public LayerMask _layerToAvoid;
        
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            _rb.AddForce(Vector3.down * (_gravityForce * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("Bazooka") || collider.gameObject.layer == _layerToAvoid) return;
            Instantiate(_explosionPrefab, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
        }
    }
}
