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
        [SerializeField] float speed = 10.0f;
        [SerializeField] float damage = 10.0f;

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

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}