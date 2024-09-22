using UnityEngine;

public class ScalePlayerOnTimeUp : MonoBehaviour
{
    public CountDown countDownScript; // Referencia a CountDown scripthez, kézzel beállítva az Inspectorban
    public GameObject player; // Referencia a player GameObjecthez, kézzel beállítva az Inspectorban
    public GameObject Wall;
    public float scaleMultiplier = 2.5f;

    void Update()
    {
        if (countDownScript != null && player != null)
        {
            if (countDownScript.remainingTime <= 0)
            {
                Collider2D wallCollider = Wall.GetComponent<Collider2D>();
                if (wallCollider != null)
                {
                    Destroy(wallCollider);
                }
                player.transform.localScale = Vector3.one * scaleMultiplier; // Mindhárom tengelyen növeljük a scale-t
            }
        }
    }
}