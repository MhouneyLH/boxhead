using Boxhead.Presentation.Game.Interfaces;
using UnityEngine;

namespace Boxhead.Presentation.Game.Enemy
{
    /// <summary>
    /// Represents an enemy.
    /// Could be a zombie, a soldier, a robot, etc.
    /// </summary>
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health = 30.0f;
        [SerializeField] private float damage = 5.0f;
        [SerializeField] private int scorePoints = 10;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0.0f)
            {
                GameManager.Instance.AddScore(scorePoints);
                Die();
            }
        }

        private void Die() => Destroy(gameObject);
    }
}