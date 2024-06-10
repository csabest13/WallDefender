using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    GameObject Player;
    public float scaleMultiplier = 0.5f; // A méretarány, amire szeretnénk csökkenteni
    public float duration = 5f;

    private Vector3 originalScale; // Az eredeti méret tárolásához

    private bool hasInteracted = false;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;

        // Elmentjük az eredeti skálát
        originalScale = Player.transform.localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player && !hasInteracted)
        {
            hasInteracted = true;
            // Skálázza a "Player" nevû GameObjectet
            StartCoroutine(ScalePlayer());
            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator ScalePlayer()
    {
        // Ellenõrizzük, hogy a "Player" objektum létezik-e
        if (Player != null)
        {
            // Számoljuk a cél skálát (fele akkora méretre)
            Vector3 targetScale = originalScale * scaleMultiplier;

            // Animáljuk a skálát feleakkorára
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                Player.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
                yield return null;
            }

            // Várunk 5 másodpercet
            yield return new WaitForSeconds(5f);

            // Animáljuk a skálát vissza az eredeti méretre
            timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                Player.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
                yield return null;
            }

            // Állítsuk vissza az interakciós zászlót
            hasInteracted = false;
        }
    }
}