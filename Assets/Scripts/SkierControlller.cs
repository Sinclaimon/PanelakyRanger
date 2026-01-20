using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
public class SkierControlller : MonoBehaviour
{

    private PlayerInputActions controls;

    private void Awake()
    {
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Skiing.Enable();
        controls.Skiing.Steer.performed += ctx => Debug.Log("Steer: " + ctx.ReadValue<float>());
    }

    private void OnDisable()
    {
        controls.Skiing.Disable();
        Debug.Log("skiing disabled");

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
