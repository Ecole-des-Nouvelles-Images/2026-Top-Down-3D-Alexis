using UnityEngine;

namespace AlexisVeVer.Scripts
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _timeToDestroy;
        private float _timeAlive;
        
        
        // Update is called once per frame
        void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive >= _timeToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
