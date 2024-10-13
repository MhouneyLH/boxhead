using UnityEngine;

namespace Boxhead.Presentation.Game.Enemy
{
    /// <summary>
    /// Responsible for moving enemies towards the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private const string PLAYER_TAG = "Player";
        private const string ANIMATOR_HORIZONTAL_NAME = "Horizontal";
        private const string ANIMATOR_LAST_HORIZONTAL_NAME = "LastHorizontal";
        private const string ANIMATOR_VERTICAL_NAME = "Vertical";
        private const string ANIMATOR_LAST_VERTICAL_NAME = "LastVertical";


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Vector2 direction = GetDirectionToPlayer();
            Move(direction);
        }

        private Vector2 GetDirectionToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            return (player.transform.position - transform.position).normalized;
        }

        private void Move(Vector2 direction)
        {
            _rigidbody.velocity = _speed * direction;
            UpdateAnimation(direction);
        }

        private void UpdateAnimation(Vector2 direction)
        {
            _animator.SetFloat(ANIMATOR_HORIZONTAL_NAME, direction.x);
            _animator.SetFloat(ANIMATOR_VERTICAL_NAME, direction.y);

            if (direction != Vector2.zero)
            {
                _animator.SetFloat(ANIMATOR_LAST_HORIZONTAL_NAME, direction.x);
                _animator.SetFloat(ANIMATOR_LAST_VERTICAL_NAME, direction.y);
            }
        }
    }
}