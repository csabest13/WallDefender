using UnityEngine;

public class Wall : MonoBehaviour
{
    public Retry retry;

    void Start()
    {
        // Megkeress�k a GameManager komponenst a scene-ben
        retry = FindObjectOfType<Retry>();
    }

    // Ez a met�dus automatikusan megh�v�dik, amikor a Wall objektum megsemmis�l
    void OnDestroy()
    {
        // �rtes�tj�k a GameManager-t, hogy a Wall megsemmis�lt
        if (retry != null)
        {
         
        }
    }
}