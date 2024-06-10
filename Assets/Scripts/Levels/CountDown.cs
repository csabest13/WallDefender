using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    //Szintek-nél visszaszámlálás - addig megy a szint, amig 0 lesz a remainingTime

    [SerializeField] TMP_Text countDownText; //visszaszámlálás
    [SerializeField] float remainingTime; //hátralévõ idõ
    public GameObject levelSucceedPanel; //pálya sikerült
    public GameObject wall; //fal
    private healthObjectLevels healthObjectLevelScript;

    void Start()
    {

        levelSucceedPanel.SetActive(false);

        if (wall != null)
        {
            healthObjectLevelScript = wall.GetComponent<healthObjectLevels>();
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        int seconds = Mathf.FloorToInt(remainingTime);
        int miliseconds = Mathf.FloorToInt((remainingTime - seconds) * 100);
        string formatted = string.Format("{0:00}:{1:00}", seconds, miliseconds);

        countDownText.text = formatted;

        if (remainingTime == 0)
        {
            levelSucceedPanel.SetActive(true);
            UnlockNewLevel();

            DisableWallIsTrigger();

        }

        if (healthObjectLevelScript != null && healthObjectLevelScript.currentHP <= 0)
        {
            return;
        }
    }

    void UnlockNewLevel()
    {
        //Unlockolni kell a következõ szint gombot, ha sikerült a pálya
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    void DisableWallIsTrigger()
    {
        BoxCollider2D wallCollider = wall.GetComponent<BoxCollider2D>();
        if (wallCollider != null)
        {
            wallCollider.isTrigger = false;
        }
    }
}