using Boxhead.Presentation.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxhead.Presentation.Player
{
    /// <summary>
    /// Responsible for moving the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private Rigidbody _rigidbody;
        private Vector2 _movementDirection = Vector2.zero;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Move.performed += OnMove;
            InputSystemManager.Instance.GameSceneActions.Player.Move.canceled += OnMove;
        }

        private void OnDisable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Move.performed -= OnMove;
            InputSystemManager.Instance.GameSceneActions.Player.Move.canceled -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.velocity = speed * _movementDirection;
            Rotate();
        }

        private void Rotate()
        {
            // don't change rotation if the player is not moving
            if (_movementDirection == Vector2.zero)
            {
                return;
            }

            float angle = Mathf.Atan2(_movementDirection.y, _movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90.0f);
        }

        public Vector2 GetMovementDirection() => _movementDirection;
    }
}