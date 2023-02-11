using System;
using Scripts.Main;
using UnityEngine;

namespace Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static event Action<Vector2> InputDirection;
        
        private void Awake()
        {
            SwipeManager.SwipeDetected += OnSwipeDetected;
        }

        private void OnSwipeDetected(Vector2 direction)
        {
            if(direction == Vector2.down)
                Debug.Log("down");
            else if(direction == Vector2.up)
                Debug.Log("up");
            else if(direction == Vector2.left)
                Debug.Log("left");
            else if(direction == Vector2.right)
                Debug.Log("right");
            
            InputDirection?.Invoke(direction);
        }
    }
}