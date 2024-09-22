using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public GameObject levelFailedPanel;
    public GameObject outOfRechargePanel;
    public Button retryButton;
    public TMP_Text attemptsText;
    public TMP_Text cooldownText;
    public healthObjectLevels healthScript;

    private int maxAttempts = 3;
    public int currentAttempts;
    public float cooldownTime = 3f * 60f; // 15 perc másodpercben
    private string attemptsKey = "Attempts";
    private string lastAttemptResetKey = "LastAttemptResetTime";
    private bool hasAttemptBeenReduced = false;
    private bool cooldownJustReset = false; // Új flag, hogy a cooldown csak egyszer történjen

    void Start()
    {
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, maxAttempts);
        CheckAttemptReset(); // Ellenõrizzük a próbálkozások visszaállítását
        UpdateAttemptsText();
        UpdateCooldownText();
        levelFailedPanel.SetActive(false);
        outOfRechargePanel.SetActive(false);
        cooldownText.gameObject.SetActive(false);
        hasAttemptBeenReduced = false;
        cooldownJustReset = false;
    }

    void Update()
    {
        // Ha a próbálkozások száma 0, akkor kezeljük a panel aktiválását és a cooldown idõt
        if (currentAttempts <= 0)
        {
            outOfRechargePanel.SetActive(true);
            levelFailedPanel.SetActive(false);
            cooldownText.gameObject.SetActive(true);

            DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
            TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

            // Frissítjük a cooldown idõt mutató szöveget
            UpdateCooldownText();

            if (!cooldownJustReset)
            {
                if (timeSinceLastReset.TotalSeconds >= cooldownTime)
                {
                    // Ha a cooldown idõ lejárt és a cooldownText 0, visszaállítjuk a próbálkozásokat
                    if (cooldownText.text.Contains("00:00"))
                    {
                        currentAttempts = maxAttempts;
                        PlayerPrefs.SetInt(attemptsKey, currentAttempts);
                        PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
                        PlayerPrefs.Save();
                        retryButton.interactable = true;
                        cooldownText.gameObject.SetActive(false);
                        levelFailedPanel.SetActive(false);

                        // Frissítjük az attemptsText-et
                        UpdateAttemptsText();

                        cooldownJustReset = true; // Jelezzük, hogy a cooldown visszaállítva

                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
        else
        {
            outOfRechargePanel.SetActive(false);
            cooldownText.gameObject.SetActive(false);
            cooldownJustReset = false; // Ha van próbálkozás, a cooldown újra elérhetõ lesz
        }

        // Ha HP <= 0 és még nem csökkentettünk próbálkozást
        if (healthScript.currentHP <= 0 && !hasAttemptBeenReduced)
        {
            OnHealthDepleted();
            hasAttemptBeenReduced = true;
        }
    }

    void OnHealthDepleted()
    {
        Debug.Log("Health depleted, reducing attempts.");
        currentAttempts--;
        PlayerPrefs.SetInt(attemptsKey, currentAttempts);
        PlayerPrefs.Save();

        if (currentAttempts <= 0)
        {
            PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
            PlayerPrefs.Save();
            retryButton.interactable = false;
        }

        UpdateAttemptsText();
    }

    void UpdateAttemptsText()
    {
        attemptsText.text = "Remaining recharges: " + currentAttempts.ToString();
    }

    void UpdateCooldownText()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        // Kiszámoljuk, mennyi idõ van hátra a következõ feltöltésig
        float timeRemaining = (float)cooldownTime - (float)timeSinceLastReset.TotalSeconds;
        if (timeRemaining < 0) timeRemaining = 0;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        cooldownText.text = $"Receiving 3 recharges in: {minutes:D2}:{seconds:D2}";
    }

    void CheckAttemptReset()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        if (timeSinceLastReset.TotalSeconds >= cooldownTime)
        {
            // Ha a cooldown idõ lejárt, az újra töltési állapotot ellenõrizzük
            if (cooldownText.text.Contains("00:00"))
            {
                currentAttempts = maxAttempts;
                PlayerPrefs.SetInt(attemptsKey, currentAttempts);
                PlayerPrefs.Save();
                UpdateAttemptsText();
            }
        }
    }
    void OnApplicationQuit()
    {
        SetHPToZero();
    }

    void SetHPToZero()
    {
        // currentHP 0-ra állítása, ami elindítja a próbálkozások csökkentését
        healthScript.currentHP = 0;
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // A játékos háttérbe helyezte a játékot, a játék megáll
            Time.timeScale = 0;
        }
        else
        {
            // A játék visszatér az elõtérbe, a játék folytatódik
            Time.timeScale = 1;
        }
    }

}