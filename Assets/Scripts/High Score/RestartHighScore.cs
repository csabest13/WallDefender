using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartHighScore : MonoBehaviour
{
    public void restartHighScore()
    {
        SceneManager.LoadScene(1);
    }
}
