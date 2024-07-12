using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Tools
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        private enum JoystickMode
        {
            Static,
            Dynamic
        }

        [SerializeField] private JoystickMode _mode;
        [SerializeField] private RectTransform _joystickBackground;
        [SerializeField] private RectTransform _joystickHandle;
        private Vector2 _inputVector;


        public void OnPointerDown(PointerEventData eventData)
        {
            if (_mode == JoystickMode.Dynamic)
            {
                _joystickBackground.position = eventData.position;
                _joystickBackground.gameObject.SetActive(true);
            }

            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputVector = Vector2.zero;
            _joystickHandle.anchoredPosition = Vector2.zero;


            if (_mode == JoystickMode.Dynamic)
            {
                _joystickBackground.gameObject.SetActive(false);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground, eventData.position,
                    eventData.pressEventCamera, out var position))
            {
                position.x = (position.x / _joystickBackground.sizeDelta.x) * 2;
                position.y = (position.y / _joystickBackground.sizeDelta.y) * 2;

                _inputVector = new Vector2(position.x, position.y);
                _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

                _joystickHandle.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.sizeDelta.x / 2),
                    _inputVector.y * (_joystickBackground.sizeDelta.y / 2));
            }
        }

        public float Horizontal()
        {
            return _inputVector.x;
        }

        public float Vertical()
        {
            return _inputVector.y;
        }

        private void Start()
        {
            if (_mode == JoystickMode.Dynamic)
            {
                _joystickBackground.gameObject.SetActive(false);
            }
        }
    }
}