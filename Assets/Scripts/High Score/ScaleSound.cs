using UnityEngine;

public class ScaleSoundEffects : MonoBehaviour
{
    public AudioClip scaleSound;
    private AudioSource audioSource;

    private float previousScaleX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        previousScaleX = transform.localScale.x;
    }

    void Update()
    {
        float currentScaleX = transform.localScale.x;

        float scaleDifferenceX = currentScaleX - previousScaleX;

        if (scaleDifferenceX != 0)
        {
            if (audioSource != null && scaleSound != null)
            {
                audioSource.PlayOneShot(scaleSound);
            }
        }
        previousScaleX = currentScaleX;
    }
}