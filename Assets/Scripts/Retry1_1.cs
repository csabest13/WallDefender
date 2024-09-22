using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class Retry2 : MonoBehaviour
{
    public GameObject levelFailedPanel;  // A "Level Failed" panel a UI-n
    public GameObject outOfRechargePanel; // Az "outofRecharge" panel a UI-n
    public Button retryButton;           // A Retry gomb a UI-n
    public TMP_Text attemptsText;        // A pr�b�lkoz�sok sz�m�t jelz� TextMeshPro UI elem
    public TMP_Text cooldownText;        // A cooldown id�t mutat� TextMeshPro UI elem

    private int maxAttempts = 3;         // Maxim�lis pr�b�lkoz�sok sz�ma
    private int currentAttempts;         // Aktu�lis pr�b�lkoz�sok sz�ma
    private float cooldownTime = 1 * 60f; // 15 perc m�sodpercben (15 perc = 900 m�sodperc)
    private string attemptsKey = "Attempts"; // Kulcs a PlayerPrefs-ben a pr�b�lkoz�sok sz�m�nak ment�s�hez
    private string lastAttemptResetKey = "LastAttemptResetTime"; // Kulcs az utols� pr�b�lkoz�s-visszat�lt�si id� t�rol�s�hoz

    void Start()
    {
        // A pr�b�lkoz�sok sz�m�nak bet�lt�se PlayerPrefs-b�l, vagy ha nincs elmentve, akkor maxAttempts
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, maxAttempts);

        // Ellen�rizz�k, hogy mennyi id� telt el az utols� pr�b�lkoz�s �ta
        CheckAttemptReset();

        // Friss�tj�k a UI-t a pr�b�lkoz�sok sz�m�val �s a cooldown id�vel
        UpdateAttemptsText();
        UpdateCooldownText();

        // A panelek alap�rtelmez�sben inakt�vak
        levelFailedPanel.SetActive(false);
        outOfRechargePanel.SetActive(false);

        // A cooldownText alap�rtelmez�sben inakt�v
        cooldownText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Ha a pr�b�lkoz�sok sz�ma 0, figyelj�k az eltelt id�t �s kezelj�k a paneleket
        if (currentAttempts <= 0)
        {
            // Az "outofRecharge" panel aktiv�l�sa �s a "Level Failed" panel deaktiv�l�sa
            outOfRechargePanel.SetActive(true);
            levelFailedPanel.SetActive(false);

            // Az id�z�t� sz�veg l�that�v� t�tele
            cooldownText.gameObject.SetActive(true);

            DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
            TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

            if (timeSinceLastReset.TotalSeconds >= cooldownTime)
            {
                currentAttempts = maxAttempts; // Minden pr�b�lkoz�s visszat�lt�se
                PlayerPrefs.SetInt(attemptsKey, currentAttempts); // Friss�tj�k a PlayerPrefs-ben
                PlayerPrefs.Save(); // Mentj�k a PlayerPrefs adatokat
                UpdateAttemptsText();

                // Ha van pr�b�lkoz�s, akkor ism�t interakt�vv� tessz�k a Retry gombot
                retryButton.interactable = true;

                // Elmentj�k az aktu�lis id�pontot
                PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
                PlayerPrefs.Save(); // Mentj�k a PlayerPrefs adatokat

                // Az id�z�t� sz�veg elrejt�se
                cooldownText.gameObject.SetActive(false);

                // A panelek vissza�ll�t�sa az alap�rtelmezett �llapotba

                levelFailedPanel.SetActive(true);
            }

            // Friss�tj�k a cooldown id�t mutat� sz�veget
            UpdateCooldownText();
        }
        else
        {
            // Ha a pr�b�lkoz�sok sz�ma nem 0, a "Level Failed" panel akt�v, az "outofRecharge" panel inakt�v
            outOfRechargePanel.SetActive(false);
            cooldownText.gameObject.SetActive(false);
        }
    }

    // Ez a f�ggv�ny h�v�dik meg, amikor a Wall objektum megsemmis�l
    public void OnWallDestroyed()
    {
        Debug.Log("Wall destroyed, reducing attempts.");  // Logoljuk, hogy a Wall megsemmis�lt
        currentAttempts--;                                // Cs�kkentj�k a pr�b�lkoz�sok sz�m�t
        PlayerPrefs.SetInt(attemptsKey, currentAttempts);  // Elmentj�k az �j pr�b�lkoz�si sz�mot PlayerPrefs-be

        // Ha a pr�b�lkoz�sok sz�ma 0, elmentj�k az aktu�lis id�pontot a visszasz�ml�l�s ind�t�s�hoz
        if (currentAttempts <= 0)
        {
            PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
            PlayerPrefs.Save(); // Mentj�k a PlayerPrefs adatokat

            // Deaktiv�ljuk a Retry gombot, mivel elfogytak a pr�b�lkoz�sok
            retryButton.interactable = false;
        }

        // Friss�tj�k a UI-t a pr�b�lkoz�sok sz�m�val
        UpdateAttemptsText();
    }

    // Friss�ti a pr�b�lkoz�sok sz�m�t mutat� sz�veget
    void UpdateAttemptsText()
    {
        attemptsText.text = currentAttempts.ToString();
    }

    // Friss�ti a cooldown id�t mutat� sz�veget
    void UpdateCooldownText()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        // Kisz�moljuk, mennyi id� van h�tra a k�vetkez� felt�lt�sig
        float timeRemaining = (float)cooldownTime - (float)timeSinceLastReset.TotalSeconds;
        if (timeRemaining < 0) timeRemaining = 0;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        cooldownText.text = $"Attempts recharging in: {minutes:D2}:{seconds:D2}";
    }

    // Ellen�rzi, hogy lej�rt-e a 15 perc az utols� reset �ta
    void CheckAttemptReset()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        if (timeSinceLastReset.TotalSeconds >= cooldownTime)
        {
            currentAttempts = maxAttempts; // Minden pr�b�lkoz�s visszat�lt�se
            PlayerPrefs.SetInt(attemptsKey, currentAttempts); // Friss�tj�k a PlayerPrefs-ben
            PlayerPrefs.Save(); // Mentj�k a PlayerPrefs adatokat
            UpdateAttemptsText();
        }
    }
}