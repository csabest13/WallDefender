using UnityEngine;
using UnityEngine.UI;

public class PauseMenuInteract : MonoBehaviour
{
    public Button mainMenuButton;  // A MainMenu gomb referenci�ja
    public Retry retryScript;      // A Retry script referenci�ja

    void Start()
    {
        // Ellen�rizz�k, hogy a Retry script megtal�lhat� legyen
        if (retryScript == null)
        {
            retryScript = FindObjectOfType<Retry>();
        }
    }

    void Update()
    {
        // Ha a cooldownTime nem 0, akkor a MainMenu gombot deaktiv�ljuk
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