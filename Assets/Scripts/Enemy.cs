using Boxhead.Interfaces;
using UnityEngine;

/// <summary>
/// Represents an enemy.
/// Could be a zombie, a soldier, a robot, etc.
/// </summary>
[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 30.0f;
    [SerializeField] float damage = 5.0f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
