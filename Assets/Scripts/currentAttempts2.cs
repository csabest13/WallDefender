using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class currentAttempts2 : MonoBehaviour
{
    public int currentAttempts;
    private string attemptsKey = "Attempts";

    void Start()
    {
        // Betöltjük az elsõ scene-ben elmentett currentAttempts értéket
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, 3); // Default 3, ha nincs elmentett érték

        // Itt folytatódik a játék logika a második scene-ben
        Debug.Log("Current attempts in Scene 2: " + currentAttempts);
    }

    public void OnWallDestroyed()
    {
        if (currentAttempts > 0)
        {
            currentAttempts--;
            PlayerPrefs.SetInt(attemptsKey, currentAttempts);
            PlayerPrefs.Save();
        }

        // További logika
    }
}