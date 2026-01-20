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


    private Rigidbody rb;
    private float steer;
    private bool brake;
    private float defaultDrag;
    private PlayerInputActions controls;

    private void Awake()
    {
        controls = new PlayerInputActions();
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.linearDamping;
    }

    private void OnEnable()
    {
        controls.Skiing.Enable();
        controls.Skiing.Steer.performed += OnSteer;
        controls.Skiing.Steer.canceled += OnSteer;
    }

    private void OnDisable()
    {
        controls.Skiing.Disable();
        Debug.Log("skiing disabled");

    }

    private void OnSteer(InputAction.CallbackContext ctx)
    {
        Debug.Log("Steer: " + ctx.ReadValue<float>());
    }

    private void OnBrake(InputAction.CallbackContext ctx)
    {
        brake = ctx.ReadValueAsButton();
    }

    private void FixedUpdate()
    {
        Vector3 downhill = Vector3.ProjectOnPlane(Vector3.down, Vector3.up);
        rb.AddForce(downhill * downhillForce, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

    }
}
