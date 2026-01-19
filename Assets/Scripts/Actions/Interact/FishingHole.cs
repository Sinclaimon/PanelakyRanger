using UnityEngine;

public class FishingHole : MonoBehaviour
{
    bool isFishing;
    int BaitType = 0;
    int chance = Random.Range(0, 100);

    public void Interact()
    {
        isFishing = true;
        Debug.Log("Fishing Interacted with");

        LockCamera();
        FishingLogic();
    }

    void LockCamera()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void FishingLogic()
    {
        if (BaitType == 0) //No bait
        {
            
        }

        else if (BaitType == 1) //Live worm bait
        {

        }

        else if (BaitType == 2) //Small fish bait
        {

        }
    }
}
