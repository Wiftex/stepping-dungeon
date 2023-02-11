using System.Collections.Generic;
using UnityEngine;

namespace Scripts.DungeonGeneration
{
    public class DungeonGenerationController : MonoBehaviour
    {
        [SerializeField] private List<AbstractDungeonGenerator> dungeonGenerators;

        public HashSet<Vector2Int> GenerateDungeon()
        {
            var currentGenerator = dungeonGenerators[Random.Range(0, dungeonGenerators.Count)];
            return currentGenerator.GenerateDungeon();
        }
    }
}