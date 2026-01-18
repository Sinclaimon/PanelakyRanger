using UnityEngine;

public class Fish : MonoBehaviour, IInteractable
{
    bool isFishing;

    public void Interact()
    {
        isFishing = true;
        Debug.Log("Fishing Interacted with");

        LockCamera();
    }

    void LockCamera()
    {

    }
}
