using Boxhead;
using Boxhead.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxhead
{
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
            // HINT: always take the CallbackContext as parameter for the method
            // otherwise the event won't get unsubscribed properly :( (Unity Bug?)
            InputSystemManager.Instance.GameSceneActions.Player.Shoot.performed += Shoot;
        }

        void OnDisable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Shoot.performed -= Shoot;
        }

        void Shoot(InputAction.CallbackContext context)
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
}