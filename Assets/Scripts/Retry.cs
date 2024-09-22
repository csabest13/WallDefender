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
    public float cooldownTime = 3f * 60f; // 15 perc m�sodpercben
    private string attemptsKey = "Attempts";
    private string lastAttemptResetKey = "LastAttemptResetTime";
    private bool hasAttemptBeenReduced = false;
    private bool cooldownJustReset = false; // �j flag, hogy a cooldown csak egyszer t�rt�njen

    void Start()
    {
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, maxAttempts);
        CheckAttemptReset(); // Ellen�rizz�k a pr�b�lkoz�sok vissza�ll�t�s�t
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
        // Ha a pr�b�lkoz�sok sz�ma 0, akkor kezelj�k a panel aktiv�l�s�t �s a cooldown id�t
        if (currentAttempts <= 0)
        {
            outOfRechargePanel.SetActive(true);
            levelFailedPanel.SetActive(false);
            cooldownText.gameObject.SetActive(true);

            DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
            TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

            // Friss�tj�k a cooldown id�t mutat� sz�veget
            UpdateCooldownText();

            if (!cooldownJustReset)
            {
                if (timeSinceLastReset.TotalSeconds >= cooldownTime)
                {
                    // Ha a cooldown id� lej�rt �s a cooldownText 0, vissza�ll�tjuk a pr�b�lkoz�sokat
                    if (cooldownText.text.Contains("00:00"))
                    {
                        currentAttempts = maxAttempts;
                        PlayerPrefs.SetInt(attemptsKey, currentAttempts);
                        PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
                        PlayerPrefs.Save();
                        retryButton.interactable = true;
                        cooldownText.gameObject.SetActive(false);
                        levelFailedPanel.SetActive(false);

                        // Friss�tj�k az attemptsText-et
                        UpdateAttemptsText();

                        cooldownJustReset = true; // Jelezz�k, hogy a cooldown vissza�ll�tva

                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
        else
        {
            outOfRechargePanel.SetActive(false);
            cooldownText.gameObject.SetActive(false);
            cooldownJustReset = false; // Ha van pr�b�lkoz�s, a cooldown �jra el�rhet� lesz
        }

        // Ha HP <= 0 �s m�g nem cs�kkentett�nk pr�b�lkoz�st
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

        // Kisz�moljuk, mennyi id� van h�tra a k�vetkez� felt�lt�sig
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
            // Ha a cooldown id� lej�rt, az �jra t�lt�si �llapotot ellen�rizz�k
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
        // currentHP 0-ra �ll�t�sa, ami elind�tja a pr�b�lkoz�sok cs�kkent�s�t
        healthScript.currentHP = 0;
    }

    void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            // A j�t�kos h�tt�rbe helyezte a j�t�kot, a j�t�k meg�ll
            Time.timeScale = 0;
        }
        else
        {
            // A j�t�k visszat�r az el�t�rbe, a j�t�k folytat�dik
            Time.timeScale = 1;
        }
    }

}