using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    private PlayerInputActions controls;
    bool isFlashlighton = false;

    void Awake()
    {
        controls = new PlayerInputActions();
    }
    
    void OnEnable()
    {
        controls.Enable();
        controls.Common.Flashlight.performed += OnF;
        //controls.(ActionMap).(Action).performed 
    }

    void OnDisable()
    {
        controls.Common.Pause.performed -= OnF;
        controls.Disable();
    }

    private void OnF(InputAction.CallbackContext context) // escape to pause using Input action
    {
        if (!isFlashlighton)
        {
            FlashlightON();
        }

        else
        {
            FlashlightOFF();
        }
    }

    private void FlashlightON()
    {
        isFlashlighton = true;
        flashlight.SetActive(true);
    }

    private void FlashlightOFF()
    {
        isFlashlighton = false;
        flashlight.SetActive(false);

    }

}
