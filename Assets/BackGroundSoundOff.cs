using UnityEngine;

public class BackgroundSoundControl : MonoBehaviour
{
    public GameObject backgroundSound; // A BackgroundSound GameObject referencia
    public GameObject defeatSound;
    public GameObject victorySound;
    public GameObject wall; //fal
    private healthObjectLevels healthObjectLevelScript;
    private CountDown countDown;



    void Update()
    {

        if (wall != null)
        {
            healthObjectLevelScript = wall.GetComponent<healthObjectLevels>();
        }

        if (healthObjectLevelScript != null && healthObjectLevelScript.currentHP <= 0)
        {
            backgroundSound.SetActive(false);
            defeatSound.SetActive(true);
        }
        if (wall != null)
        {
            countDown = wall.GetComponent<CountDown>();
        }

        if (countDown != null && countDown.remainingTime <= 0)
        {
            backgroundSound.SetActive(false);
            victorySound.SetActive(true);
        }
    }
}