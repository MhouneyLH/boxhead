using Boxhead;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Responsible for moving the player.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    new Rigidbody rigidbody;
    Vector2 movementDirection = Vector2.zero;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        InputSystemManager.GameSceneActions.Player.Move.performed += ctx => OnMove(ctx);
        InputSystemManager.GameSceneActions.Player.Move.canceled += ctx => OnMove(ctx);
    }

    void OnDisable()
    {
        InputSystemManager.GameSceneActions.Player.Move.performed -= ctx => OnMove(ctx);
        InputSystemManager.GameSceneActions.Player.Move.canceled -= ctx => OnMove(ctx);
    }

    void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rigidbody.MovePosition(rigidbody.position + speed * Time.fixedDeltaTime * (Vector3)movementDirection);
    }
}
