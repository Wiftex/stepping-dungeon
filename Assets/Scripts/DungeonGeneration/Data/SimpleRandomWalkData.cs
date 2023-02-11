using UnityEngine;

namespace Scripts.DungeonGeneration.Data
{
    [CreateAssetMenu(fileName ="SimpleRandomWalkParameters_",menuName = "DungeonGeneration/SimpleRandomWalkData")]
    public class SimpleRandomWalkData : ScriptableObject
    {
        public int iterations = 10, walkLength = 10;
        public bool startRandomlyEachIteration = true;
    }
}
