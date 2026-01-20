using UnityEngine;
using UnityEngine.InputSystem;

public class SkierController : MonoBehaviour
{

    private PlayerInputActions controls;

    private void Awake()
    {
        controls = new PlayerInputActions();
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
