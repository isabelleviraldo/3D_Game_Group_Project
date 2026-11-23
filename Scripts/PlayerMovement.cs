using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;     // how fast the player walks
    public float turnSpeed = 720f;   // how fast the player turns to face a direction
    public float gravity = -20f;     // keeps the player on the floor

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Read the keyboard (WASD or arrow keys)
        Vector2 input = Vector2.zero;
        var kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.wKey.isPressed || kb.upArrowKey.isPressed)    input.y += 1;
            if (kb.sKey.isPressed || kb.downArrowKey.isPressed)  input.y -= 1;
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) input.x += 1;
            if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  input.x -= 1;
        }

        // 2. Turn the input into a direction on the floor
        Vector3 move = new Vector3(input.x, 0f, input.y);
        if (move.sqrMagnitude > 1f) move.Normalize();

        // 3. Turn the player to face the way they're walking
        if (move != Vector3.zero)
        {
            Quaternion target = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeed * Time.deltaTime);
        }

        // 4. Gravity so the player stays on the ground
        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        // 5. Actually move
        Vector3 velocity = move * moveSpeed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
