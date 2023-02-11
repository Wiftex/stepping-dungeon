using System.Collections.Generic;
using Scripts.DungeonGeneration.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.DungeonGeneration
{
    public class ObjectsSpawner : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        private SpawnObjectsData _data;

        public void Spawn(SpawnObjectsData data, HashSet<Vector2Int> floor)
        {
            _data = Instantiate(data);
        }
        
    }
}