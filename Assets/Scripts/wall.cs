using UnityEngine;

public class Wall : MonoBehaviour
{
    public Retry retry;

    void Start()
    {
        // Megkeressük a GameManager komponenst a scene-ben
        retry = FindObjectOfType<Retry>();
    }

    // Ez a metódus automatikusan meghívódik, amikor a Wall objektum megsemmisül
    void OnDestroy()
    {
        // Értesítjük a GameManager-t, hogy a Wall megsemmisült
        if (retry != null)
        {
         
        }
    }
}