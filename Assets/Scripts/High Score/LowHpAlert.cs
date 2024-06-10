using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHpAlert : MonoBehaviour
{
    // Piros háttér, alacsony HP-n villogás effekt - alfa érték változtatás

    [SerializeField] private SpriteRenderer LowHPsign;
    [SerializeField] private healthObjectHighScore currentHpCheckHighScore;

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
        if (currentHpCheckHighScore.currentHP <= 25)
        {
            float alphaChangeRate = 200f / 255f / blinkDuration * Time.deltaTime;
            Color color = LowHPsign.color;

            if (increasingAlpha)
            {
                color.a += alphaChangeRate;
                if (color.a >= 200f / 255f)
                {
                    color.a = 200f / 255f;
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
        else
        {
            Color color = LowHPsign.color;
            color.a = 0f;
            LowHPsign.color = color;
        }
    }
}