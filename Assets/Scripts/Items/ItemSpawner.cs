using System.Collections.Generic;
using Terrain;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<TerrainFloatAway> _spawnableTerrains;
        [FormerlySerializedAs("_spawnableObjects")] [SerializeField] private List<GameObject> _spawnableItems;

        [SerializeField] private GameObject _apparitionFx;
        [SerializeField] private Vector3 _itemSpawnOffset;
        [SerializeField] private float _timeBetweenSpawns;
        [SerializeField] private float _dropSpeed;

        private GameObject _indic;
        private GameObject _spawnedItem;
        private float _timeSinceLastSpawn;

        // Update is called once per frame
        void Update()
        {
            for(int i = _spawnableTerrains.Count - 1; i >= 0; i--)
            {
                if (_spawnableTerrains[i].FloatsAway)
                {
                    _spawnableTerrains.RemoveAt(i);
                }
            }
        
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= _timeBetweenSpawns)
            {
                GameObject randomTerrain = _spawnableTerrains[Random.Range(0, _spawnableItems.Count)].gameObject;
            
                _indic = Instantiate(_apparitionFx, randomTerrain.transform.position, transform.rotation);
                _spawnedItem = Instantiate(_spawnableItems[Random.Range(0, _spawnableItems.Count)], 
                    randomTerrain.transform.position + _itemSpawnOffset, Quaternion.identity);
                _timeSinceLastSpawn = 0;
            }
        
            if (_spawnedItem != null && _indic != null)
            {
                _spawnedItem.transform.position = Vector3.MoveTowards(_spawnedItem.transform.position,
                    _indic.transform.position, _dropSpeed * Time.deltaTime);
            }
        }
    }
}
