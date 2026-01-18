using UnityEngine;
using UnityEngine.InputSystem;

public class BasicCamera : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;   
    public Camera playerCamera;    

    [Header("Settings")]
    public float lookSensitivity = 0.5f;
    public float maxLookAngle = 90f;

    private PlayerInputActions controls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    void Awake()
    {
        controls = new PlayerInputActions();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Traversal.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Traversal.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        // Vertical 
        xRotation -= lookInput.y * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal 
        playerBody.Rotate(Vector3.up * lookInput.x * lookSensitivity);
    }
}
