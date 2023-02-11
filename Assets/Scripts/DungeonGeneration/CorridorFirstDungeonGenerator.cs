using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.DungeonGeneration
{
    public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
    {
        [SerializeField]
        private int corridorLength = 14, corridorCount = 5;
        [SerializeField]
        [Range(0.1f,1)]
        private float roomPercent = 0.8f;

        protected override HashSet<Vector2Int> RunProceduralGeneration()
        {
            return CorridorFirstGeneration();
        }

        private HashSet<Vector2Int> CorridorFirstGeneration()
        {
            var floorPositions = new HashSet<Vector2Int>();
            var potentialRoomPositions = new HashSet<Vector2Int>();

            CreateCorridors(floorPositions, potentialRoomPositions);

            var roomPositions = CreateRooms(potentialRoomPositions);

            var deadEnds = FindAllDeadEnds(floorPositions);

            CreateRoomsAtDeadEnd(deadEnds, roomPositions);

            floorPositions.UnionWith(roomPositions);

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

            return floorPositions;
        }

        private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach (var position in deadEnds)
            {
                if(roomFloors.Contains(position) == false)
                {
                    var room = RunRandomWalk(randomWalkParameters, position);
                    roomFloors.UnionWith(room);
                }
            }
        }

        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            var deadEnds = new List<Vector2Int>();
            foreach (var position in floorPositions)
            {
                var neighboursCount = 0;
                foreach (var direction in Direction2D.cardinalDirectionsList)
                {
                    if (floorPositions.Contains(position + direction))
                        neighboursCount++;
                
                }
                if (neighboursCount == 1)
                    deadEnds.Add(position);
            }
            return deadEnds;
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            var roomPositions = new HashSet<Vector2Int>();
            var roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);
            var roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }
            return roomPositions;
        }

        private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
        {
            var currentPosition = startPosition;
            potentialRoomPositions.Add(currentPosition);

            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                currentPosition = corridor[^1];
                potentialRoomPositions.Add(currentPosition);
                floorPositions.UnionWith(corridor);
            }
        }
    }
}
