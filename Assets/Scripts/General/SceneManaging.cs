using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void playHighScore()
    {
        SceneManager.LoadScene(1);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void gameRules()
    {
        SceneManager.LoadScene(3);
    }
    public void playLevels()
    {
        SceneManager.LoadScene(4);
    }
}
