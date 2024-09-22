using UnityEngine;

public class PauseGameOnPanel : MonoBehaviour
{
    public GameObject outOfRechargePanel; // A panel hivatkoz�sa

    void Update()
    {
        if (outOfRechargePanel.activeSelf)
        {
            // Meg�ll�tjuk a j�t�kot, de a cooldownText-hez DateTime-et fogunk haszn�lni
            Time.timeScale = 0f;
        }
        else
        {
            // Ha a panel inakt�v, a j�t�k folytat�dik
            Time.timeScale = 1f;
        }
    }
}