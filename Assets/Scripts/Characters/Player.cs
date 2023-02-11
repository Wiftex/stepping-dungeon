using System.Collections.Generic;
using Scripts.Stats;
using UnityEngine;

namespace Scripts.Characters
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private AgentMover agentMover;
        [SerializeField] private float moveDuration;
        [SerializeField] private Stat health;
        [SerializeField] private Stat damage;

        public bool IsMoving { get; private set; }

        public Stat Health => health;
        
        private HashSet<Vector2Int> _floorPositions;
        private Vector2Int _currentPosition;

        public void TakeDamage(int value)
        {
            Health.Value -= value;
        }
        
        private void OnEnable()
        {
            PlayerController.InputDirection += OnInputDirection;
            Health.ValueChanged += HealthOnValueChanged;
        }

        private void HealthOnValueChanged(int value)
        {
            Debug.Log("Player health: " + value);
        }

        private void OnDisable()
        {
            PlayerController.InputDirection -= OnInputDirection;
            Health.ValueChanged -= HealthOnValueChanged;
        }

        public void SetFloorPositions(Vector2Int startPosition, HashSet<Vector2Int> positions)
        {
            _currentPosition = startPosition;
            _floorPositions = positions;
            
            var position = (Vector2)_currentPosition;
            position.y +=  1f;
            position.x += transform.lossyScale.x / 2;
            transform.position = position;
        }
        
        private void OnInputDirection(Vector2 direction)
        {
            var nextPosition = _currentPosition + (Vector2Int.CeilToInt(direction));
            if(_floorPositions.Contains(nextPosition) && !IsMoving)
            {
                var movePosition = _currentPosition + direction;
                movePosition.y +=  1f;
                movePosition.x += transform.lossyScale.x / 2;
                Move(movePosition);
                _currentPosition = nextPosition;
            }
        }

        public void Move(Vector2 position)
        {
            IsMoving = true;
            agentMover.Move(position, moveDuration, OnMoveComplete);
        }

        private void OnMoveComplete() => IsMoving = false;
    }
}