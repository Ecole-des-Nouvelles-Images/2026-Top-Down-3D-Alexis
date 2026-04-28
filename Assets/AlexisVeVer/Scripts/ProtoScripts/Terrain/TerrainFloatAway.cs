using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Terrain
{
    public class TerrainFloatAway : MonoBehaviour
    { 
        [SerializeField] private float _floatAwayDistance;
        [SerializeField] private float _speedToFloatAway;

        public bool FloatsAway = false;
        
        
        // Update is called once per frame
        void Update()
        {
            if (FloatsAway)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    transform.position * _floatAwayDistance, _speedToFloatAway);
            }
        }
    }
}
