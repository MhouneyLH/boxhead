using Boxhead;
using Boxhead.Interfaces;
using UnityEngine;

/// <summary>
/// Represents the player.
/// The prefab should have a Player-Tag on it.
/// </summary>
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] float _health = 100.0f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _weapon;

    void OnEnable()
    {
        InputSystemManager.GameSceneActions.Player.Shoot.performed += _ => Shoot();
    }

    void OnDisable()
    {
        InputSystemManager.GameSceneActions.Player.Shoot.performed -= _ => Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _weapon.transform.position, _weapon.transform.rotation);
        bullet.GetComponent<IProjectile>().Launch(transform.up);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0.0f)
        {
            Die();
            GameManager.Instance.ResetGame();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
