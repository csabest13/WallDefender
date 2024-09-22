using UnityEngine;
using UnityEngine.UI;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public SpriteRenderer targetSpriteRenderer;
    public float sensitivity = 200f;

    private float[] samples = new float[256];

    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);

        float sum = 0;
        foreach (var sample in samples)
        {
            sum += sample;
        }
        float average = sum / samples.Length;

        Color color = targetSpriteRenderer.color;
        color.g = Mathf.Clamp01(average * sensitivity);
        targetSpriteRenderer.color = color;
    }
}

