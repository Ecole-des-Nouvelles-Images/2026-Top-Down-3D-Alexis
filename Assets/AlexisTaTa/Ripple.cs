using UnityEngine;

public class Ripple : MonoBehaviour
{
    public RippleTest rippleSender;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            Vector3 hitPoint = transform.position;
            rippleSender.SpawnRipple(hitPoint);
        }
    }

}
