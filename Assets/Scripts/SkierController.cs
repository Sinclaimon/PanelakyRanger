using UnityEngine;
using UnityEngine.InputSystem;

public class SkierController : MonoBehaviour
{
    [Header("Tuning")]
    [SerializeField] private float downhillForce = 25f;
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float lateralDamping = 8f;
    [SerializeField] private float steerTorque = 6f;
    [SerializeField] private float brakeDrag = 3f;

    [Header("References")]
    private Rigidbody rb;
    public InputHub input;
    private float steer;
    private bool brake;
    private float defaultDrag;

    private void Awake()
    {
        if (input == null)
            input = GetComponent<InputHub>();
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.linearDamping;
    }

    private void OnSteer(InputAction.CallbackContext ctx)
    {
        Debug.Log("Steer: " + ctx.ReadValue<float>());
    }

    private void OnBrake(InputAction.CallbackContext ctx)
    {
        brake = ctx.ReadValueAsButton();
    }

    private void Start()
    {
        rb.AddForce(transform.forward * 20f, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {
        Vector3 downhill = Vector3.ProjectOnPlane(Vector3.down, Vector3.up);
        rb.AddForce(downhill * downhillForce, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        if (input == null) return;

        // Only run skiing movement when hub is in Skiing mode
        if (input.CurrentMode != InputHub.Mode.Skiing) return;

        float steer = input.Steer;

        // Placeholder downhill push (we will make this slope-aware next)
        rb.AddForce(Vector3.forward * downhillForce, ForceMode.Acceleration);

        // Steering
        rb.AddTorque(Vector3.up * steer * steerTorque, ForceMode.Acceleration);

        // Lateral damping for carving feel
        Vector3 localVel = transform.InverseTransformDirection(rb.linearVelocity);
        localVel.x = Mathf.MoveTowards(localVel.x, 0f, lateralDamping * Time.fixedDeltaTime);
        rb.linearVelocity = transform.TransformDirection(localVel);

        // Speed clamp
        float speed = rb.linearVelocity.magnitude;
        if (speed > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

    }

}
