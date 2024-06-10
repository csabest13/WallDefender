using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timeHandler : MonoBehaviour
{
    //Score = idõ. - GameOver Scene -  Float és String értékben is szükséges tárolni az idõt, mert ki is kell iratni és össze is kell hasonlitani

    public GameObject currentTime;
    public GameObject highTime;

    private TMP_Text currentTimeText;
    private TMP_Text highTimeText;

    private float currentTimeFloat;
    private float highTimeFloat;

    private string currentTimeString;
    private string highTimeString;

    void Start()
    {
        currentTimeText = currentTime.GetComponent<TMP_Text>();
        highTimeText = highTime.GetComponent<TMP_Text>();

        currentTimeString = PlayerPrefs.GetString("currentTimeString");
        currentTimeText.text = currentTimeString;

        currentTimeFloat = PlayerPrefs.GetFloat("currentTimeFloat");
        highTimeFloat = PlayerPrefs.GetFloat("highTimeFloat");

        // a jelenlegi idõ nagyobb-e, mint a legmagasabb idõ, és frissítjük 
        if (highTimeFloat < currentTimeFloat)
        {
            PlayerPrefs.SetString("highTimeString", currentTimeString);
            PlayerPrefs.SetFloat("highTimeFloat", currentTimeFloat);
        }

        // a legmagasabb idõ szövege
        highTimeString = "Highest " + PlayerPrefs.GetString("highTimeString");
        highTimeText.text = highTimeString;
    }

}