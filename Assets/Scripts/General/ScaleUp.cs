using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    //Scalelõ potik - Player Gameobject scale változás + destroy poti

    GameObject Player;
    public float scaleIncrease = 0.5f;

    private bool hasInteracted = false;

    private void Start()
    {
        Player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player && !hasInteracted)
        {
            hasInteracted = true;
            ScalePlayer();
            Destroy(gameObject);
        }
    }

    void ScalePlayer()
    {
        if (Player != null)
        {
            Vector3 originalScale = Player.transform.localScale;
            Vector3 targetScale = new Vector3(originalScale.x + scaleIncrease, originalScale.y + scaleIncrease, originalScale.z);

            Player.transform.localScale = targetScale;

            if (Player.transform.localScale.x <= 0)
            {
                Vector3 fixedScale = Player.transform.localScale;
                fixedScale.x = 0;
                Player.transform.localScale = fixedScale;
            }
        }
    }

}