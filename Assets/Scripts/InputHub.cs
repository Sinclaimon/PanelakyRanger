using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHub : MonoBehaviour
{
    public enum Mode { Fishing, Skiing }

    public Mode CurrentMode { get; private set; } = Mode.Fishing;

    public Vector2 Look { get; private set; }
    public float Steer { get; private set; }

    public event Action<Mode> ModeChanged;

    private PlayerInputActions controls;

    private void Awake()
    {
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Enable();

        // Keep camera look on Fishing for now, since that's what your asset has.
        controls.Common.Look.performed += OnLook;
        controls.Common.Look.canceled += OnLook;

        controls.Skiing.Steer.performed += OnSteer;
        controls.Skiing.Steer.canceled += OnSteer;

        SetMode(CurrentMode);
    }

    private void OnDisable()
    {
        if (controls == null) return;

        controls.Common.Look.performed -= OnLook;
        controls.Common.Look.canceled -= OnLook;

        controls.Skiing.Steer.performed -= OnSteer;
        controls.Skiing.Steer.canceled -= OnSteer;

        controls.Disable();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SetMode(CurrentMode == Mode.Fishing ? Mode.Skiing : Mode.Fishing);
        }

        Debug.Log("Current Mode: " + CurrentMode);
    }

    public void SetMode(Mode mode)
    {
        CurrentMode = mode;

        controls.Fishing.Disable();
        controls.Skiing.Disable();

        Steer = 0f;

        if (mode == Mode.Fishing) controls.Fishing.Enable();
        else controls.Skiing.Enable();

        ModeChanged?.Invoke(mode);
    }

    private void OnLook(InputAction.CallbackContext ctx) => Look = ctx.ReadValue<Vector2>();
    private void OnSteer(InputAction.CallbackContext ctx) => Steer = ctx.ReadValue<float>();
}
