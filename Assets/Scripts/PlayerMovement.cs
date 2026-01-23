using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float skiSpeed = 7f;
    [SerializeField] private float acceleration = 12f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 540f;

    private CharacterController controller;
    private Vector3 currentVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 moveInput = new Vector3(input.x, 0f, input.y);
        bool isSkiing = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float targetSpeed = isSkiing ? skiSpeed : walkSpeed;

        if (moveInput.sqrMagnitude > 0.001f)
        {
            Vector3 desiredDirection = moveInput.normalized;
            Vector3 desiredVelocity = desiredDirection * targetSpeed;
            currentVelocity = Vector3.MoveTowards(currentVelocity, desiredVelocity, acceleration * Time.deltaTime);
            controller.Move(currentVelocity * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
        }
    }
}
