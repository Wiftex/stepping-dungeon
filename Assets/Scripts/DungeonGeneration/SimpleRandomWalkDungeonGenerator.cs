using System.Collections.Generic;
using System.Linq;
using Scripts.DungeonGeneration.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.DungeonGeneration
{
    public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
    {

        [SerializeField]
        protected SimpleRandomWalkData randomWalkParameters;


        protected override HashSet<Vector2Int> RunProceduralGeneration()
        {
            var floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

            return floorPositions;
        }

        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
        {
            var currentPosition = position;
            var floorPositions = new HashSet<Vector2Int>();
            for (int i = 0; i < parameters.iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
                floorPositions.UnionWith(path);
                if (parameters.startRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
            return floorPositions;
        }

    }
}
