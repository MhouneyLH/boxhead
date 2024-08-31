using UnityEngine;

namespace Boxhead.Presentation.Game.Enemy
{
    /// <summary>
    /// Responsible for moving enemies towards the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] float _speed = 1.0f;

        private Rigidbody _rigidbody;

        private const string PLAYER_TAG = "Player";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = GetDirectionToPlayer();
            Move(direction);
            Rotate(direction);
        }

        Vector3 GetDirectionToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            return (player.transform.position - transform.position).normalized;
        }

        private void Move(Vector3 direction)
        {
            _rigidbody.velocity = _speed * direction;
        }

        private void Rotate(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90.0f);
        }
    }
}