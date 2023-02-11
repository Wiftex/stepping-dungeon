using System.Collections.Generic;
using UnityEngine;

namespace Scripts.DungeonGeneration
{
    public abstract class AbstractDungeonGenerator : MonoBehaviour
    {
        [SerializeField]
        protected TilemapVisualizer tilemapVisualizer = null;
        [SerializeField]
        protected Vector2Int startPosition = Vector2Int.zero;

        public HashSet<Vector2Int> GenerateDungeon()
        {
            tilemapVisualizer.Clear();
            return RunProceduralGeneration();
        }

        protected abstract HashSet<Vector2Int> RunProceduralGeneration();
    }
}
