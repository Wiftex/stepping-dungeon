using System;
using UnityEngine;

namespace Scripts.Main
{
    public class SwipeManager : MonoBehaviour
    {
        [SerializeField] private float deadZone;
        
        public static event Action<Vector2> SwipeDetected;

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;
        private bool _isSwiping;
        private bool _isMobile;

        private void Start()
        {
            _isMobile = Application.isMobilePlatform;
        }

        private void Update()
        {
            if (_isMobile)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        _isSwiping = true;
                        _tapPosition = Input.GetTouch(0).position;
                    }
                    else if (Input.GetTouch(0).phase is TouchPhase.Canceled or TouchPhase.Ended)
                        ResetSwipe();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _isSwiping = true;
                    _tapPosition = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                    ResetSwipe();
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            if (_isSwiping)
            {
                _swipeDelta = _isMobile
                    ? Input.GetTouch(0).position - _tapPosition
                    : (Vector2)Input.mousePosition - _tapPosition;
            }

            if(_swipeDelta.magnitude > deadZone)
            {
                Vector2 swipeDirection;
                if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                    swipeDirection = _swipeDelta.x > 0 ? Vector2.right : Vector2.left;
                else
                    swipeDirection = _swipeDelta.y > 0 ? Vector2.up : Vector2.down;
                SwipeDetected?.Invoke(swipeDirection);

                ResetSwipe();
            }
        }

        private void ResetSwipe()
        {
            _swipeDelta = Vector2.zero;
            _tapPosition = Vector2.zero;
            _isSwiping = false;
        }
    }
}