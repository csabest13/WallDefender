using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    GameObject Player;
    public float scaleMultiplier = 0.5f; // A m�retar�ny, amire szeretn�nk cs�kkenteni
    public float duration = 5f;

    private Vector3 originalScale; // Az eredeti m�ret t�rol�s�hoz

    private bool hasInteracted = false;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;

        // Elmentj�k az eredeti sk�l�t
        originalScale = Player.transform.localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player && !hasInteracted)
        {
            hasInteracted = true;
            // Sk�l�zza a "Player" nev� GameObjectet
            StartCoroutine(ScalePlayer());
            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator ScalePlayer()
    {
        // Ellen�rizz�k, hogy a "Player" objektum l�tezik-e
        if (Player != null)
        {
            // Sz�moljuk a c�l sk�l�t (fele akkora m�retre)
            Vector3 targetScale = originalScale * scaleMultiplier;

            // Anim�ljuk a sk�l�t feleakkor�ra
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                Player.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
                yield return null;
            }

            // V�runk 5 m�sodpercet
            yield return new WaitForSeconds(5f);

            // Anim�ljuk a sk�l�t vissza az eredeti m�retre
            timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                float t = timer / duration;
                Player.transform.localScale = Vector3.Lerp(targetScale, originalScale, t);
                yield return null;
            }

            // �ll�tsuk vissza az interakci�s z�szl�t
            hasInteracted = false;
        }
    }
}