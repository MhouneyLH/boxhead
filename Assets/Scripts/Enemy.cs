using Boxhead.Interfaces;
using UnityEngine;

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

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
