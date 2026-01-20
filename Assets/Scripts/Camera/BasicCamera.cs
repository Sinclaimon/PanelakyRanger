using UnityEngine;

public class BasicCamera : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;
    public Camera playerCamera;
    public InputHub input;

    [Header("Settings")]
    public float lookSensitivity = 0.5f;
    public float maxLookAngle = 90f;

    private float xRotation;


    private void Awake()
    {
        if (input == null)
            input = GetComponentInParent<InputHub>();

        if (input == null)
            Debug.LogError("BasicCamera: No InputHub found in parent hierarchy.");
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (input == null || playerBody == null || playerCamera == null) return;

        Vector2 look = input.Look;

        xRotation -= look.y * lookSensitivity;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * look.x * lookSensitivity);
    }
}
