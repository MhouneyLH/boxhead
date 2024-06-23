using Boxhead.Interfaces;
using UnityEngine;

namespace Boxhead
{
    /// <summary>
    /// Represents a bullet that can be shot from a player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour, IProjectile
    {
        [SerializeField] float _speed = 10.0f;
        [SerializeField] float _damage = 10.0f;

        public void Launch(Vector3 direction)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = _speed * direction;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Player>())
            {
                return;
            }

            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
                Destroy(gameObject);
                return;
            }

            Debug.Log($"Bullet flying through Collider: {other.name}");
        }
    }
}