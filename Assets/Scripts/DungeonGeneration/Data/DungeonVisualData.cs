using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.DungeonGeneration.Data
{
    [CreateAssetMenu(fileName = "DungeonVisual_", menuName = "DungeonGeneration/VisualData")]
    public class DungeonVisualData : ScriptableObject
    {
        [SerializeField] private List<TileBase> floorTiles;
        [SerializeField] private TileBase wallTop;
        [SerializeField] private TileBase wallSideRight;
        [SerializeField] private TileBase wallSideLeft;
        [SerializeField] private TileBase wallBottom;
        [SerializeField] private TileBase wallFull;
        [SerializeField] private TileBase wallInnerCornerDownLeft;
        [SerializeField] private TileBase wallInnerCornerDownRight;
        [SerializeField] private TileBase wallDiagonalCornerDownRight;
        [SerializeField] private TileBase wallDiagonalCornerDownLeft;
        [SerializeField] private TileBase wallDiagonalCornerUpRight;
        [SerializeField] private TileBase wallDiagonalCornerUpLeft;

        public List<TileBase> FloorTiles => floorTiles;

        public TileBase WallTop => wallTop;

        public TileBase WallSideRight => wallSideRight;

        public TileBase WallSideLeft => wallSideLeft;

        public TileBase WallBottom => wallBottom;

        public TileBase WallFull => wallFull;

        public TileBase WallInnerCornerDownLeft => wallInnerCornerDownLeft;

        public TileBase WallInnerCornerDownRight => wallInnerCornerDownRight;

        public TileBase WallDiagonalCornerDownRight => wallDiagonalCornerDownRight;

        public TileBase WallDiagonalCornerDownLeft => wallDiagonalCornerDownLeft;

        public TileBase WallDiagonalCornerUpRight => wallDiagonalCornerUpRight;

        public TileBase WallDiagonalCornerUpLeft => wallDiagonalCornerUpLeft;
    }
}