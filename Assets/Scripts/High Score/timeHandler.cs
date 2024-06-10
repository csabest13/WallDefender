using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timeHandler : MonoBehaviour
{
    //Score = id�. - GameOver Scene -  Float �s String �rt�kben is sz�ks�ges t�rolni az id�t, mert ki is kell iratni �s �ssze is kell hasonlitani

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

        // a jelenlegi id� nagyobb-e, mint a legmagasabb id�, �s friss�tj�k 
        if (highTimeFloat < currentTimeFloat)
        {
            PlayerPrefs.SetString("highTimeString", currentTimeString);
            PlayerPrefs.SetFloat("highTimeFloat", currentTimeFloat);
        }

        // a legmagasabb id� sz�vege
        highTimeString = "Highest " + PlayerPrefs.GetString("highTimeString");
        highTimeText.text = highTimeString;
    }

}