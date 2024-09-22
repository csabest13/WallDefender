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
        // Bet�ltj�k az els� scene-ben elmentett currentAttempts �rt�ket
        currentAttempts = PlayerPrefs.GetInt(attemptsKey, 3); // Default 3, ha nincs elmentett �rt�k

        // Itt folytat�dik a j�t�k logika a m�sodik scene-ben
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

        // Tov�bbi logika
    }
}