using Boxhead.Interfaces;
using UnityEngine;

namespace Boxhead
{
    /// <summary>
    /// Represents an enemy.
    /// Could be a zombie, a soldier, a robot, etc.
    /// </summary>
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] float _health = 30.0f;
        [SerializeField] float _damage = 5.0f;
        [SerializeField] int _scorePoints = 10;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == gameObject.layer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0.0f)
            {
                GameManager.Instance.AddScore(_scorePoints);
                Die();
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}