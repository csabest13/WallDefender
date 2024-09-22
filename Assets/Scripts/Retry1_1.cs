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
    public TMP_Text attemptsText;        // A próbálkozások számát jelzõ TextMeshPro UI elem
    public TMP_Text cooldownText;        // A cooldown idõt mutató TextMeshPro UI elem

    private int maxAttempts = 3;         // Maximális próbálkozások száma
    private int currentAttempts;         // Aktuális próbálkozások száma
    private float cooldownTime = 1 * 60f; // 15 perc másodpercben (15 perc = 900 másodperc)
    private string attemptsKey = "Attempts"; // Kulcs a PlayerPrefs-ben a próbálkozások számának mentéséhez
    private string lastAttemptResetKey = "LastAttemptResetTime"; // Kulcs az utolsó próbálkozás-visszatöltési idõ tárolásához

    void Start()
    {
        // A próbálkozások számának betöltése PlayerPrefs-bõl, vagy ha nincs elmentve, akkor maxAttempts
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, maxAttempts);

        // Ellenõrizzük, hogy mennyi idõ telt el az utolsó próbálkozás óta
        CheckAttemptReset();

        // Frissítjük a UI-t a próbálkozások számával és a cooldown idõvel
        UpdateAttemptsText();
        UpdateCooldownText();

        // A panelek alapértelmezésben inaktívak
        levelFailedPanel.SetActive(false);
        outOfRechargePanel.SetActive(false);

        // A cooldownText alapértelmezésben inaktív
        cooldownText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Ha a próbálkozások száma 0, figyeljük az eltelt idõt és kezeljük a paneleket
        if (currentAttempts <= 0)
        {
            // Az "outofRecharge" panel aktiválása és a "Level Failed" panel deaktiválása
            outOfRechargePanel.SetActive(true);
            levelFailedPanel.SetActive(false);

            // Az idõzítõ szöveg láthatóvá tétele
            cooldownText.gameObject.SetActive(true);

            DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
            TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

            if (timeSinceLastReset.TotalSeconds >= cooldownTime)
            {
                currentAttempts = maxAttempts; // Minden próbálkozás visszatöltése
                PlayerPrefs.SetInt(attemptsKey, currentAttempts); // Frissítjük a PlayerPrefs-ben
                PlayerPrefs.Save(); // Mentjük a PlayerPrefs adatokat
                UpdateAttemptsText();

                // Ha van próbálkozás, akkor ismét interaktívvá tesszük a Retry gombot
                retryButton.interactable = true;

                // Elmentjük az aktuális idõpontot
                PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
                PlayerPrefs.Save(); // Mentjük a PlayerPrefs adatokat

                // Az idõzítõ szöveg elrejtése
                cooldownText.gameObject.SetActive(false);

                // A panelek visszaállítása az alapértelmezett állapotba

                levelFailedPanel.SetActive(true);
            }

            // Frissítjük a cooldown idõt mutató szöveget
            UpdateCooldownText();
        }
        else
        {
            // Ha a próbálkozások száma nem 0, a "Level Failed" panel aktív, az "outofRecharge" panel inaktív
            outOfRechargePanel.SetActive(false);
            cooldownText.gameObject.SetActive(false);
        }
    }

    // Ez a függvény hívódik meg, amikor a Wall objektum megsemmisül
    public void OnWallDestroyed()
    {
        Debug.Log("Wall destroyed, reducing attempts.");  // Logoljuk, hogy a Wall megsemmisült
        currentAttempts--;                                // Csökkentjük a próbálkozások számát
        PlayerPrefs.SetInt(attemptsKey, currentAttempts);  // Elmentjük az új próbálkozási számot PlayerPrefs-be

        // Ha a próbálkozások száma 0, elmentjük az aktuális idõpontot a visszaszámlálás indításához
        if (currentAttempts <= 0)
        {
            PlayerPrefs.SetString(lastAttemptResetKey, DateTime.Now.ToString());
            PlayerPrefs.Save(); // Mentjük a PlayerPrefs adatokat

            // Deaktiváljuk a Retry gombot, mivel elfogytak a próbálkozások
            retryButton.interactable = false;
        }

        // Frissítjük a UI-t a próbálkozások számával
        UpdateAttemptsText();
    }

    // Frissíti a próbálkozások számát mutató szöveget
    void UpdateAttemptsText()
    {
        attemptsText.text = currentAttempts.ToString();
    }

    // Frissíti a cooldown idõt mutató szöveget
    void UpdateCooldownText()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        // Kiszámoljuk, mennyi idõ van hátra a következõ feltöltésig
        float timeRemaining = (float)cooldownTime - (float)timeSinceLastReset.TotalSeconds;
        if (timeRemaining < 0) timeRemaining = 0;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        cooldownText.text = $"Attempts recharging in: {minutes:D2}:{seconds:D2}";
    }

    // Ellenõrzi, hogy lejárt-e a 15 perc az utolsó reset óta
    void CheckAttemptReset()
    {
        DateTime lastResetTime = DateTime.Parse(PlayerPrefs.GetString(lastAttemptResetKey, DateTime.Now.ToString()));
        TimeSpan timeSinceLastReset = DateTime.Now - lastResetTime;

        if (timeSinceLastReset.TotalSeconds >= cooldownTime)
        {
            currentAttempts = maxAttempts; // Minden próbálkozás visszatöltése
            PlayerPrefs.SetInt(attemptsKey, currentAttempts); // Frissítjük a PlayerPrefs-ben
            PlayerPrefs.Save(); // Mentjük a PlayerPrefs adatokat
            UpdateAttemptsText();
        }
    }
}