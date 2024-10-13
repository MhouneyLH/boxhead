using Boxhead.Presentation.Game.Interfaces;
using UnityEngine;

namespace Boxhead.Presentation.Game
{
    /// <summary>
    /// Represents a bullet that can be shot from a player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour, IProjectile
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private float damage = 10.0f;

        private const string OBSTACLE_TAG_NAME = "Obstacle";

        public void Launch(Vector2 direction)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = speed * direction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>())
            {
                return;
            }

            if (other.CompareTag(OBSTACLE_TAG_NAME))
            {
                Destroy(gameObject);
            }
            else if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}