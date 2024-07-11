using UnityEngine;

public class RandomMusicSelector : MonoBehaviour
{
    public AudioClip[] musicClips; // Lista az AudioClip-ek t�rol�s�ra
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicClips.Length > 0)
        {
            // V�letlenszer�en kiv�laszt egy AudioClipet a list�b�l
            int randomIndex = Random.Range(0, musicClips.Length);
            AudioClip selectedClip = musicClips[randomIndex];

            // Be�ll�tja a kiv�lasztott AudioClipet az AudioSource-re �s lej�tssza
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Nincs AudioClip a musicClips list�ban!");
        }
    }
}
