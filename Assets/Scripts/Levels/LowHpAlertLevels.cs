using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHpAlertLevels : MonoBehaviour
{
    // Piros h�tt�r, alacsony HP-n villog�s effekt - alfa �rt�k v�ltoztat�s
    [SerializeField] private SpriteRenderer LowHPsign;
    [SerializeField] private healthObjectLevels currentHpCheckLevels;

    private float blinkDuration = 1f; 
    private bool increasingAlpha = true;

    void Start()
    {
        SetInitialAlpha();
    }

    void SetInitialAlpha()
    {
        if (LowHPsign != null)
        {
            Color initialColor = LowHPsign.color;
            initialColor.a = 0f;
            LowHPsign.color = initialColor;
        }
    }

    void Update()
    {
        if (currentHpCheckLevels.currentHP == 0)
        {
            ResetAlpha();
        }
        else if (currentHpCheckLevels.currentHP <= 25)
        {
            Blink();
        }
        else
        {
            ResetAlpha();
        }
    }

    void Blink()
    {
        float alphaChangeRate = 100f / 255f / blinkDuration * Time.deltaTime;
        Color color = LowHPsign.color;

        if (increasingAlpha)
        {
            color.a += alphaChangeRate;
            if (color.a >= 100f / 255f)
            {
                color.a = 100f / 255f;
                increasingAlpha = false;
            }
        }
        else
        {
            color.a -= alphaChangeRate;
            if (color.a <= 0f)
            {
                color.a = 0f;
                increasingAlpha = true;
            }
        }

        LowHPsign.color = color;
    }

    void ResetAlpha()
    {
        Color color = LowHPsign.color;
        color.a = 0f;
        LowHPsign.color = color;
    }
}