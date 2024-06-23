using UnityEngine;

/// <summary>
/// Responsible for moving enemies towards the player.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

    const string PLAYER_TAG = "Player";

    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 direction = GetDirectionToPlayer();
        rigidbody.velocity = direction * speed;
        Rotate(direction);
    }

    void Rotate(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90.0f);
    }

    Vector3 GetDirectionToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        return (player.transform.position - transform.position).normalized;
    }
}
