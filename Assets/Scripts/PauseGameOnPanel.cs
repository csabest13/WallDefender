using UnityEngine;

public class PauseGameOnPanel : MonoBehaviour
{
    public GameObject outOfRechargePanel; // A panel hivatkozása

    void Update()
    {
        if (outOfRechargePanel.activeSelf)
        {
            // Megállítjuk a játékot, de a cooldownText-hez DateTime-et fogunk használni
            Time.timeScale = 0f;
        }
        else
        {
            // Ha a panel inaktív, a játék folytatódik
            Time.timeScale = 1f;
        }
    }
}