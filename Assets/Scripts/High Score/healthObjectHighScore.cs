using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class healthObjectHighScore : MonoBehaviour
{
    //Fal-ra vonatkozó tulajdonoságok

    [SerializeField] int startHP; //kezdõ élet
    [SerializeField] TMP_Text healthText; //HP text
    [SerializeField] TMP_Text timerText; //timer text
    public float timer = 0f;

    public int currentHP; //jelenlegi HP

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<Sprite> sprites;

    [SerializeField] AudioClip lowHealthSound; 
    private AudioSource audioSource; 
    private int previousHP; 

    [SerializeField] AudioClip hpDecreaseBy5Sound; //nyil hang
    [SerializeField] AudioClip hpDecreaseBy10Sound; //kard hang
    [SerializeField] AudioClip hpDecreaseBy20Sound; //bomba hang
    [SerializeField] AudioClip hpDecreaseBy100Sound; //halálfej hang
    [SerializeField] AudioClip hpIncreaseBy5Sound; //heal hang

    private bool hasPlayedHpIncreaseBy5Sound = false; //Változó, ami segit, hogy Startban ne fusson le a heal sound effect!

    void Start()
    {
        currentHP = startHP;

        PlayerPrefs.SetFloat("currentTimeFloat", 0);
        PlayerPrefs.SetString("currentTimeString", "0");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateHealthText();
        UpDateSprite();
    }
    void Update()
    {
        UpdateHealthText();
        UpDateSprite();

        if (previousHP != currentHP)
        {
            int hpDifference = previousHP - currentHP;
            if (currentHP <= 25 && previousHP > 25)
            {
                PlaySound(lowHealthSound);
            }

            // Hang lejátszása a HP csökkenéséhez
            if (hpDifference >= 5)
            {
                PlaySound(hpDecreaseBy5Sound);
            }
            if (hpDifference >= 10)
            {
                PlaySound(hpDecreaseBy10Sound);
            }
            if (hpDifference >= 20)
            {
                PlaySound(hpDecreaseBy20Sound);
            }
            if (hpDifference >= 100)
            {
                PlaySound(hpDecreaseBy100Sound);
            }
            if (hpDifference <= -5 && hasPlayedHpIncreaseBy5Sound)
            {
                PlaySound(hpIncreaseBy5Sound);
            }
            else
            {
                hasPlayedHpIncreaseBy5Sound = true;
            }

            previousHP = currentHP;
        }

        if (currentHP <= 0)
            Destroy(gameObject);

        timer += Time.deltaTime;
        TimerText();

        PlayerPrefs.SetFloat("currentTimeFloat", timer);
        PlayerPrefs.SetString("currentTimeString", timerText.text);

        if (currentHP <= 0)
            SceneManager.LoadScene(2);
    }
    private void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHP;
    }
    void TimerText()
    {
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);
        string formattedTime = string.Format("{0:00}:{1:00}", seconds, milliseconds);

        timerText.text = "Elapsed Time: " + formattedTime;
    }

    void UpDateSprite()
    {
        //Fal sprite frissitése, ha csökken a currentHP

        // EZ IGY NEM VOLT JÓ!!!!!!!
            //if (spriteRenderer == null)
            //    return;
            //if (sprites == null || sprites.Count == 0)
            //    return;

            //float healthRate = (float)currentHP / startHP;  // 0-1
            //healthRate = 1 - healthRate;  // 1-0
            //int index = Mathf.RoundToInt(healthRate * (sprites.Count - 1));  // (sprites.Count-1) - 0 

            //spriteRenderer.sprite = sprites[index];

        if (spriteRenderer == null)
            return;
        if (sprites == null || sprites.Count == 0)
            return;

        if(currentHP <= 100 && currentHP > 85)
            spriteRenderer.sprite = sprites[0];
        else if(currentHP <= 85 && currentHP > 70)
            spriteRenderer.sprite = sprites[1];
        else if(currentHP <= 70 && currentHP > 55)
            spriteRenderer.sprite = sprites[2];
        else if (currentHP <= 55 && currentHP > 40)
            spriteRenderer.sprite = sprites[3];
        else if (currentHP <= 40 && currentHP > 20)
            spriteRenderer.sprite = sprites[4];
        else if (currentHP <= 20)
            spriteRenderer.sprite = sprites[5];
    }
    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}