using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    private Rigidbody2D body;
    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.ClampMagnitude(moveInput, 1f);
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;

        body.MovePosition(body.position + movement);
    }
}
