using Boxhead.Presentation.InputSystem;
using Boxhead.Presentation.Game.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxhead.Presentation.Game.Player
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
        [SerializeField] private float health = 100.0f;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject weapon;

        private void OnEnable()
        {
            // HINT: always take the CallbackContext as parameter for the method
            // otherwise the event won't get unsubscribed properly :( (Unity Bug?)
            InputSystemManager.Instance.GameSceneActions.Player.Shoot.performed += Shoot;
        }

        private void OnDisable()
        {
            InputSystemManager.Instance.GameSceneActions.Player.Shoot.performed -= Shoot;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            GameObject bullet = Instantiate(bulletPrefab, weapon.transform.position, weapon.transform.rotation);
            bullet.GetComponent<IProjectile>().Launch(transform.up);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;

            if (health <= 0.0f)
            {
                Die();
                GameManager.Instance.ResetGame();
            }
        }

        private void Die() => Destroy(gameObject);
    }
}