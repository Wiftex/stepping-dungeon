using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Scripts.Characters;
using Scripts.DungeonGeneration;
using Scripts.DungeonGeneration.Data;
using UnityEngine;

namespace Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private DungeonGenerationController dungeonGenerationController;
        [SerializeField] private Player playerPrefab;
        [SerializeField] private DungeonVisualData visualData;
        [SerializeField] private TilemapVisualizer tilemapVisualizer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private HashSet<Vector2Int> _floorPositions;
        private Player _player;
        private void Start()
        {
            tilemapVisualizer.SetVisualData(visualData);
            _floorPositions = dungeonGenerationController.GenerateDungeon();

            var startPositionIndex = _floorPositions.ToList()[0];
            _player = Instantiate(playerPrefab);
            _player.SetFloorPositions(startPositionIndex, _floorPositions);
            virtualCamera.Follow = _player.transform;
        }
    }
}