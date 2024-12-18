/*using UnityEngine;
using UnityEngine.InputSystem;

public class VRPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;  // Movement speed
    public float rotationSpeed = 60f;  // Turning speed

    [Header("Input Actions")]
    public InputActionProperty moveInput;  // Joystick input for movement
    public InputActionProperty rotateInput;  // Joystick input for turning

    [Header("References")]
    public CharacterController characterController;  // Add a CharacterController to XR Origin
    public Transform vrCamera;  // Reference to the VR Camera

    private void Start()
    {
        if (characterController == null)
        {
            Debug.LogError("CharacterController is not assigned!");
        }

        if (vrCamera == null)
        {
            Debug.LogError("VR Camera reference is not assigned!");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        // Get joystick input for movement
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        // Calculate forward and sideways movement
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        forward.y = 0f;  // Ensure movement stays horizontal
        right.y = 0f;

        Vector3 movement = (forward * input.y + right * input.x).normalized * moveSpeed * Time.deltaTime;

        // Apply movement
        characterController.Move(movement);
    }

    private void HandleRotation()
    {
        // Get joystick input for turning
        float turnInput = rotateInput.action.ReadValue<Vector2>().x;

        // Apply rotation around the Y-axis
        transform.Rotate(Vector3.up, turnInput * rotationSpeed * Time.deltaTime);
    }
}*/



using UnityEngine;
using UnityEngine.InputSystem;

public class VRPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;       // Horizontal movement speed
    public float rotationSpeed = 60f;  // Rotation speed
    public float verticalSpeed = 2f;   // Speed for vertical movement (up/down)

    [Header("Input Actions")]
    public InputActionProperty moveInput;       // Input for forward/side movement
    public InputActionProperty rotateInput;     // Input for rotation
    public InputActionProperty rightGripInput;  // Right grip for moving up
    public InputActionProperty leftGripInput;   // Left grip for moving down

    [Header("References")]
    public CharacterController characterController;  // Reference to the CharacterController
    public Transform vrCamera;                       // Reference to the VR Camera

    private void Start()
    {
        if (characterController == null)
            Debug.LogError("CharacterController is not assigned!");

        if (vrCamera == null)
            Debug.LogError("VR Camera reference is not assigned!");
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleVerticalMovement();
    }

    private void HandleMovement()
    {
        // Get joystick input for movement
        Vector2 input = moveInput.action.ReadValue<Vector2>();

        // Calculate forward and sideways movement
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        forward.y = 0f;  // Keep movement horizontal
        right.y = 0f;

        Vector3 movement = (forward * input.y + right * input.x).normalized * moveSpeed * Time.deltaTime;

        // Apply movement
        characterController.Move(movement);
    }

    private void HandleRotation()
    {
        // Get joystick input for rotation
        float turnInput = rotateInput.action.ReadValue<Vector2>().x;

        // Apply rotation around the Y-axis
        transform.Rotate(Vector3.up, turnInput * rotationSpeed * Time.deltaTime);
    }

    private void HandleVerticalMovement()
    {
        // Grip button values
        float rightGripValue = rightGripInput.action.ReadValue<float>(); // Right grip for UP
        float leftGripValue = leftGripInput.action.ReadValue<float>();   // Left grip for DOWN

        Vector3 verticalMovement = Vector3.zero;

        if (rightGripValue > 0.1f) // Move UP when right grip is pressed
        {
            verticalMovement = Vector3.up * rightGripValue * verticalSpeed * Time.deltaTime;
        }
        else if (leftGripValue > 0.1f) // Move DOWN when left grip is pressed
        {
            verticalMovement = Vector3.down * leftGripValue * verticalSpeed * Time.deltaTime;
        }

        // Apply vertical movement
        characterController.Move(verticalMovement);
    }
}
