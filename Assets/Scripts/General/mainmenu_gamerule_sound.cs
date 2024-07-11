using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu_gamerule_sound : MonoBehaviour
{
    private static mainmenu_gamerule_sound instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ha az aktuális scene nem a "main menu" vagy a "game rule", megsemmisítjük a MusicPlayer-t
        if (scene.name != "Main menu" && scene.name != "GameRules" && scene.name != "PlayLevels")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}
