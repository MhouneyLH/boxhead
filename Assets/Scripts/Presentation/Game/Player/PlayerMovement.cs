using Boxhead.Presentation.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxhead.Presentation.Game.Player
{
    /// <summary>
    /// Responsible for moving the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5.0f;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private InputAction _movementInputAction;

        private Vector2 _movementDirection = Vector2.zero;

        private const string ANIMATOR_HORIZONTAL_NAME = "Horizontal";
        private const string ANIMATOR_LAST_HORIZONTAL_NAME = "LastHorizontal";
        private const string ANIMATOR_VERTICAL_NAME = "Vertical";
        private const string ANIMATOR_LAST_VERTICAL_NAME = "LastVertical";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _movementInputAction = InputSystemManager.Instance.GameSceneActions.Player.Move;
        }

        private void OnEnable()
        {
            _movementInputAction.performed += OnMove;
            _movementInputAction.canceled += OnMove;
        }

        private void OnDisable()
        {
            _movementInputAction.performed -= OnMove;
            _movementInputAction.canceled -= OnMove;
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
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            _animator.SetFloat(ANIMATOR_HORIZONTAL_NAME, _movementDirection.x);
            _animator.SetFloat(ANIMATOR_VERTICAL_NAME, _movementDirection.y);

            if (_movementDirection != Vector2.zero)
            {
                _animator.SetFloat(ANIMATOR_LAST_HORIZONTAL_NAME, _movementDirection.x);
                _animator.SetFloat(ANIMATOR_LAST_VERTICAL_NAME, _movementDirection.y);
            }
        }

        public Vector2 GetLastMovementDirection() => new(_animator.GetFloat(ANIMATOR_LAST_HORIZONTAL_NAME),
                                                         _animator.GetFloat(ANIMATOR_LAST_VERTICAL_NAME));
    }
}