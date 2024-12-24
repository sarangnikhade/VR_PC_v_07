using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonNetworkedController : NetworkBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float jumpHeight = 2f; // Jump height
    public float gravity = -9.81f; // Gravity

    public float mouseSensitivity = 100f; // Mouse sensitivity

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>()?.transform;

        if (cameraTransform == null)
        {
            Debug.LogError("Camera not found for player prefab.");
            return;
        }

        // Disable the camera for non-owners only
        if (!IsOwner)
        {
            cameraTransform.gameObject.SetActive(false);
        }
        else
        {
            cameraTransform.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        HandleMouseLook();
        HandleMovement();
    }

    private void HandleMouseLook()
    {
        if (cameraTransform == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -40f, 36f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
