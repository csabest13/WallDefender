using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMute : MonoBehaviour
{
    private GameObject audioListenerObj;

    private void Start()
    {
        audioListenerObj = GameObject.Find("AudioListener");
    }

    public void Onclick()
    {
        if (audioListenerObj != null)
        {
            if (audioListenerObj.GetComponent<AudioListener>() != null)
            {
                Destroy(audioListenerObj.GetComponent<AudioListener>());
            }
            else
            {
                audioListenerObj.AddComponent<AudioListener>();
            }
        }
    }
}