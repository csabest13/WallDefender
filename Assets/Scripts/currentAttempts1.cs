using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class currentAttempts1 : MonoBehaviour
{
    public int currentAttempts = 3;
    private string attemptsKey = "Attempts";

    void Start()
    {
        // Ha már létezett currentAttempts az elõzõ játékból, akkor betöltjük
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, currentAttempts);
    }

    public void OnWallDestroyed()
    {
        if (currentAttempts > 0)
        {
            currentAttempts--;
            PlayerPrefs.SetInt(attemptsKey, currentAttempts);
            PlayerPrefs.Save();
        }

        // További logika, ha a játékos elhasználta az összes próbálkozást
    }

    public void LoadNextScene()
    {
        // Elmentjük az aktuális próbálkozásokat
        PlayerPrefs.SetInt(attemptsKey, currentAttempts);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scene2"); // Betöltjük a második scene-t
    }
}