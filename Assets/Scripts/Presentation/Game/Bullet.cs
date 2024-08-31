using Boxhead.Presentation.Game.Interfaces;
using UnityEngine;

namespace Boxhead.Presentation.Game
{
    /// <summary>
    /// Represents a bullet that can be shot from a player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour, IProjectile
    {
        [SerializeField] float speed = 10.0f;
        [SerializeField] float damage = 10.0f;

        public void Launch(Vector3 direction)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = speed * direction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player.Player>())
            {
                return;
            }

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            Debug.Log($"Bullet flying through Collider: {other.name}");
        }
    }
}