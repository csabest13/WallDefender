using UnityEngine;
using UnityEngine.UI;

public class PauseMenuInteract : MonoBehaviour
{
    public Button mainMenuButton;  // A MainMenu gomb referenciája
    public Retry retryScript;      // A Retry script referenciája

    void Start()
    {
        // Ellenõrizzük, hogy a Retry script megtalálható legyen
        if (retryScript == null)
        {
            retryScript = FindObjectOfType<Retry>();
        }
    }

    void Update()
    {
        // Ha a cooldownTime nem 0, akkor a MainMenu gombot deaktiváljuk
        if (retryScript.cooldownTime != 0)
        {
            mainMenuButton.interactable = false;
        }
        else
        {
            mainMenuButton.interactable = true;
        }
    }
}