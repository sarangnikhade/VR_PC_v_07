// File: Scripts/FirstPersonController.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    // Variables for player movement
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    // Variables for mouse look
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    // References
    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Get references to the CharacterController and Camera
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep the player grounded
        }

        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction based on input and transform direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Adjust the x-axis rotation and clamp it to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera and the player body
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
