using UnityEngine;

public class MuteManager : MonoBehaviour
{
    public static MuteManager Instance;
    public bool IsMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        AudioListener.pause = IsMuted;
    }
}