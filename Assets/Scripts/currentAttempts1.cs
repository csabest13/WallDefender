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
        // Ha m�r l�tezett currentAttempts az el�z� j�t�kb�l, akkor bet�ltj�k
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

        // Tov�bbi logika, ha a j�t�kos elhaszn�lta az �sszes pr�b�lkoz�st
    }

    public void LoadNextScene()
    {
        // Elmentj�k az aktu�lis pr�b�lkoz�sokat
        PlayerPrefs.SetInt(attemptsKey, currentAttempts);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Scene2"); // Bet�ltj�k a m�sodik scene-t
    }
}