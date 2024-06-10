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

    void Start()
    {
        levelFailedPanel.SetActive(false);

        currentHP = startHP;

        UpdateHealthText();
    }
    void Update()
    {
        UpdateHealthText();
        UpDateSprite();

        if (currentHP <= 0)
            levelFailedPanel.SetActive(true);
        if (currentHP <= 0)
            Destroy(gameObject);

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
}

