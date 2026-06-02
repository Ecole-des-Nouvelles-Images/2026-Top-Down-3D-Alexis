using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Terrain
{
    public class TerrainFloatAway : MonoBehaviour
    { 
        [SerializeField] private GameObject _generalTerrain;
        [SerializeField] private float _floatAwayDistance;
        [SerializeField] private float _speedToFloatAway;
        

        public bool FloatsAway = false;
        private Vector3 _targetPosition;


        void Awake()
        {
            _targetPosition = new Vector3(_generalTerrain.transform.position.x - transform.position.x * _floatAwayDistance, 
                0, _generalTerrain.transform.position.z - transform.position.z * _floatAwayDistance);
        }
        // Update is called once per frame
        void Update()
        {
            if (FloatsAway)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    _targetPosition, _speedToFloatAway);
            }
        }
    }
}