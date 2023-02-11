using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class AgentMover : MonoBehaviour 
    {
        [SerializeField] private Transform movableTransform;

        private Coroutine _moveCoroutine;

        public void Move(Vector2 position, float duration, Action onComplete)
        {
            if(_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            _moveCoroutine = StartCoroutine(MoveCoroutine(position, duration, onComplete));
        }

        private IEnumerator MoveCoroutine(Vector2 position, float duration, Action onComplete)
        {
            var counter = 0f;
            var startPos = movableTransform.position;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                movableTransform.position = Vector3.Lerp(startPos, position, counter / duration);
                yield return null;
            }

            onComplete?.Invoke();
        }
    }
}