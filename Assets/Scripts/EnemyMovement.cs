using UnityEngine;

/// <summary>
/// Responsible for moving enemies towards the player.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _speed = 1.0f;

    new Rigidbody rigidbody;

    const string PLAYER_TAG = "Player";

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = GetDirectionToPlayer();
        Move(direction);
        Rotate(direction);
    }

    Vector3 GetDirectionToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        return (player.transform.position - transform.position).normalized;
    }

    void Move(Vector3 direction)
    {
        rigidbody.velocity = _speed * direction;
        // rigidbody.MovePosition(transform.position + _speed * Time.fixedDeltaTime * direction);
        // transform.Translate(_speed * Time.fixedDeltaTime * direction);
    }

    void Rotate(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90.0f);
    }
}
