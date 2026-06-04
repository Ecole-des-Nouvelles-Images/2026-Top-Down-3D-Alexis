using Player;
using UnityEngine;

namespace Items
{
    public class FanPush : MonoBehaviour
    {
        [SerializeField] private float _ventilationForce;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().rb.AddForce(transform.up * _ventilationForce);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().rb.AddForce(transform.up * _ventilationForce * Time.deltaTime, ForceMode.Impulse);
            }
        }
    }
}