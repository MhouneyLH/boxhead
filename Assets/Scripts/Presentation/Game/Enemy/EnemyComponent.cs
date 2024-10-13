using Boxhead.Domain.Models;
using Boxhead.Presentation.Game.Interfaces;
using UnityEngine;

namespace Boxhead.Presentation.Game.Enemy
{
    /// <summary>
    /// Represents an enemy.
    /// Could be a zombie, a soldier, a robot, etc.
    /// </summary>
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyComponent : MonoBehaviour, IDamageable
    {
        private EnemyConfiguration _enemyConfiguration;

        private const int SCORE_POINT_FOR_ONE_ENEMY = 10;

        public void Initialize(EnemyConfiguration enemyConfiguration) => _enemyConfiguration = enemyConfiguration;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer)
            {
                return;
            }

            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_enemyConfiguration.Damage);
            }
        }

        public void TakeDamage(float damage)
        {
            _enemyConfiguration = _enemyConfiguration with { Health = _enemyConfiguration.Health - damage };

            if (!_enemyConfiguration.IsAlive())
            {
                GameManager.Instance.AddScore(SCORE_POINT_FOR_ONE_ENEMY);
                Die();
            }
        }

        private void Die() => Destroy(gameObject);
    }
}