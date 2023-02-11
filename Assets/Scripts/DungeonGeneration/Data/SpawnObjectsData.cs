using System.Collections.Generic;
using Scripts.Characters;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.DungeonGeneration.Data
{
    [CreateAssetMenu(fileName ="SpawnObjectsData_",menuName = "DungeonGeneration/SpawnObjectsData")]
    public class SpawnObjectsData : ScriptableObject
    {
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private List<TileBase> props;
        [SerializeField] private TileBase startTile;
        [SerializeField] private TileBase finishTile;

        public List<Enemy> Enemies => enemies;

        public List<TileBase> Props => props;

        public TileBase StartTile => startTile;

        public TileBase FinishTile => finishTile;
    }
}