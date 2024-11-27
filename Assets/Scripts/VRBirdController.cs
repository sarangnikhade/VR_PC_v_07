using UnityEngine;
using UnityEngine.InputSystem;

public class VRBirdController : MonoBehaviour
{
    public float flightSpeed = 5f; // Base forward flight speed
    public float verticalLift = 3f; // Vertical lift speed when flapping
    public float turnSpeed = 60f; // Turning speed based on joystick input

    public InputActionProperty leftFlap;  // Input for left hand (flap)
    public InputActionProperty rightFlap; // Input for right hand (flap)
    public InputActionProperty joystick; // Input for steering (joystick)

    private Vector3 direction;

    void Update()
    {
        HandleFlapping();
        HandleSteering();
        MoveBird();
    }

    private void HandleFlapping()
    {
        float leftInput = leftFlap.action.ReadValue<float>();
        float rightInput = rightFlap.action.ReadValue<float>();

        // Add lift if either hand is flapping
        if (leftInput > 0.1f || rightInput > 0.1f)
        {
            direction += Vector3.up * verticalLift * Time.deltaTime;
        }
    }

    private void HandleSteering()
    {
        Vector2 steeringInput = joystick.action.ReadValue<Vector2>();
        float turnAmount = steeringInput.x * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnAmount, 0);
    }

    private void MoveBird()
    {
        // Move forward and apply accumulated direction changes
        transform.position += transform.forward * flightSpeed * Time.deltaTime;
        transform.position += direction;

        // Smoothly reset the direction to prevent constant lift
        direction = Vector3.Lerp(direction, Vector3.zero, Time.deltaTime);
    }
}
