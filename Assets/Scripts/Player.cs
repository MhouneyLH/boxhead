using Boxhead.Interfaces;
using UnityEngine;

/// <summary>
/// Represents the player.
/// The prefab should have a Player-Tag on it.
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float health = 100.0f;
    [SerializeField] float damage = 10.0f;

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
