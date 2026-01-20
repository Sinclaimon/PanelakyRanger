using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private PlayerInputActions controls;
    private bool isPaused;

    void Awake()
    {
        controls = new PlayerInputActions();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Common.Pause.performed += OnPause;
        //controls.(ActionMap).(Action).performed 
    }

    void OnDisable()
    {
        controls.Common.Pause.performed -= OnPause;
        controls.Disable();
    }

    private void OnPause(InputAction.CallbackContext context) // escape to pause using Input action
    {
        if (!isPaused)
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame() 
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
