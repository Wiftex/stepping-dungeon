using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.DungeonGeneration
{
    public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
    {
        [SerializeField]
        private int minRoomWidth = 4, minRoomHeight = 4;
        [SerializeField]
        private int dungeonWidth = 20, dungeonHeight = 20;
        [SerializeField]
        [Range(0,10)]
        private int offset = 1;
        [SerializeField]
        private bool randomWalkRooms = false;

        protected override HashSet<Vector2Int> RunProceduralGeneration()
        {
            return CreateRooms();
        }

        private HashSet<Vector2Int> CreateRooms()
        {
            var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

            HashSet<Vector2Int> floor;
            if (randomWalkRooms)
            {
                floor = CreateRoomsRandomly(roomsList);
            }
            else
            {
                floor = CreateSimpleRooms(roomsList);
            }
            
            var roomCenters = new List<Vector2Int>();
            foreach (var room in roomsList)
            {
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            }

            var corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);

            tilemapVisualizer.PaintFloorTiles(floor);
            WallGenerator.CreateWalls(floor, tilemapVisualizer);

            return floor;
        }

        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
        {
            var floor = new HashSet<Vector2Int>();
            foreach (var roomBounds in roomsList)
            {
                var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
                foreach (var position in roomFloor)
                {
                    if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                    {
                        floor.Add(position);
                    }
                }
            }
            return floor;
        }

        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            var corridors = new HashSet<Vector2Int>();
            var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count > 0)
            {
                var closest = FindClosestPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                var newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }
            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            var corridor = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridor.Add(position);
            while (position.y != destination.y)
            {
                if(destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else if(destination.y < position.y)
                {
                    position += Vector2Int.down;
                }
                corridor.Add(position);
            }
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }else if(destination.x < position.x)
                {
                    position += Vector2Int.left;
                }
                corridor.Add(position);
            }
            return corridor;
        }

        private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            var closest = Vector2Int.zero;
            var distance = float.MaxValue;
            foreach (var position in roomCenters)
            {
                var currentDistance = Vector2.Distance(position, currentRoomCenter);
                if(currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }
            return closest;
        }

        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
        {
            var floor = new HashSet<Vector2Int>();
            foreach (var room in roomsList)
            {
                for (int col = offset; col < room.size.x - offset; col++)
                {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                        var position = (Vector2Int)room.min + new Vector2Int(col, row);
                        floor.Add(position);
                    }
                }
            }
            return floor;
        }
    }
}
