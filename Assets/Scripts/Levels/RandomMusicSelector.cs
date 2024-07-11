using UnityEngine;

public class RandomMusicSelector : MonoBehaviour
{
    public AudioClip[] musicClips; // Lista az AudioClip-ek tárolására
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicClips.Length > 0)
        {
            // Véletlenszerûen kiválaszt egy AudioClipet a listából
            int randomIndex = Random.Range(0, musicClips.Length);
            AudioClip selectedClip = musicClips[randomIndex];

            // Beállítja a kiválasztott AudioClipet az AudioSource-re és lejátssza
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Nincs AudioClip a musicClips listában!");
        }
    }
}
