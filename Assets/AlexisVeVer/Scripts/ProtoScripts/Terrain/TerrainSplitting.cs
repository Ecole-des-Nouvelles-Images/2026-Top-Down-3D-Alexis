using System.Collections.Generic;
using UnityEngine;

namespace AlexisVeVer.Scripts.ProtoScripts.Terrain
{
    public class TerrainSplitting : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _terrains;
        
        [SerializeField] private List<Transform> _terrainsTransform;
        
        private GameObject _terrainToSplit;

        private int _terrainToRemove;

        [ContextMenu("SplitTerrain")]
        public void SplitTerrain()
        {
            int terraincounter = 0;
            foreach (GameObject terrain in _terrains)
            {
                _terrainsTransform.Add(terrain.transform);
            }

            foreach (Transform terrain in _terrainsTransform)
            {
                if (_terrainToSplit == null || 
                    new Vector3(Mathf.Abs(terrain.position.x), Mathf.Abs(terrain.position.y), Mathf.Abs(terrain.position.z)).magnitude > _terrainToSplit.transform.position.magnitude)
                {
                    _terrainToSplit = terrain.gameObject;
                    _terrainToRemove = terraincounter;
                }

                terraincounter++;
            }

            if (_terrainToSplit != null)
            {
                _terrains.Remove(_terrains[_terrainToRemove]);
                _terrainsTransform.Clear();
                _terrainToSplit.GetComponent<TerrainFloatAway>().FloatsAway = true;
                _terrainToSplit = null;
            }
        }
    }
}