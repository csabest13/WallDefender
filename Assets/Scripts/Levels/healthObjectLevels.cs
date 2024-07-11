using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class healthObjectLevels : MonoBehaviour
{
    [SerializeField] int startHP;
    [SerializeField] TMP_Text healthText;
    public GameObject levelFailedPanel;

    public int currentHP;

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

    private bool hasPlayedHpIncreaseBy5Sound = false;

    void Start()
    {
        levelFailedPanel.SetActive(false);
        currentHP = startHP;

        UpdateHealthText();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        UpdateHealthText();
        UpDateSprite();

        if (currentHP <= 0)
            levelFailedPanel.SetActive(true);
     
        if (currentHP <= 0)
            Destroy(gameObject);

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

    }
    private void UpdateHealthText()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHP;
    }
    void UpDateSprite()
    {
        if (spriteRenderer == null)
            return;
        if (sprites == null || sprites.Count == 0)
            return;

        if (currentHP <= 100 && currentHP > 85)
            spriteRenderer.sprite = sprites[0];
        else if (currentHP <= 85 && currentHP > 70)
            spriteRenderer.sprite = sprites[1];
        else if (currentHP <= 70 && currentHP > 55)
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

