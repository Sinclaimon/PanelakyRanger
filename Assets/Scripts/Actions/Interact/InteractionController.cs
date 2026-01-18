using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject interactionUI;
    [SerializeField] InputActionReference interactAction;

    [Header("Settings")]
    [SerializeField] float interactionDistance = 5f;

    IInteractable currentTargetedInteractable;

    void OnEnable()
    {
        interactAction.action.Enable();
    }

    void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Update()
    {
        UpdateInteractable();
        UpdateInteractionText();
        CheckForInput();
    }

    void UpdateInteractable()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            currentTargetedInteractable = hit.collider.GetComponent<IInteractable>();
        }
        else
        {
            currentTargetedInteractable = null;
        }
    }

    void UpdateInteractionText()
    {
        interactionUI.SetActive(currentTargetedInteractable != null);
    }

    void CheckForInput()
    {
        if (currentTargetedInteractable == null)
            return;

        if (interactAction.action.WasPressedThisFrame())
        {
            currentTargetedInteractable.Interact();
        }
    }
}
