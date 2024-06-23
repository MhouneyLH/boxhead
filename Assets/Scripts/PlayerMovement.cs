using Boxhead;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxhead
{
    /// <summary>
    /// Responsible for moving the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _speed = 5.0f;

        new Rigidbody rigidbody;
        Vector2 movementDirection = Vector2.zero;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Move.performed += OnMove;
            InputSystemManager.Instance.GameSceneActions.Player.Move.canceled += OnMove;
        }

        void OnDisable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Move.performed -= OnMove;
            InputSystemManager.Instance.GameSceneActions.Player.Move.canceled -= OnMove;
        }

        void OnMove(InputAction.CallbackContext context)
        {
            movementDirection = context.ReadValue<Vector2>();
        }

        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            rigidbody.velocity = _speed * movementDirection;
            Rotate();
        }

        void Rotate()
        {
            // don't change rotation if the player is not moving
            if (movementDirection == Vector2.zero)
            {
                return;
            }

            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90.0f);
        }

        public Vector2 GetMovementDirection()
        {
            return movementDirection;
        }
    }
}