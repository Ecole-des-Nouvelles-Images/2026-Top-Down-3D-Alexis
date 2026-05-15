using UnityEngine;

public class RippleTest : MonoBehaviour
{
     public Renderer waterRenderer;
    
        Material mat;
    
        void Start()
        {
            mat = waterRenderer.material;
        }
    
        public void SpawnRipple(Vector3 worldHit)
        {
            Vector3 localHit =
                waterRenderer.transform.InverseTransformPoint(worldHit);
    
            mat.SetVector("_ImpactPosition", localHit);
            mat.SetFloat("_RippleStartTime", Time.time);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                SpawnRipple(transform.position);
            }
        }
}
