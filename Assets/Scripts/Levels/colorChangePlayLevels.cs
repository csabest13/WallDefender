using System.Collections;
using UnityEngine;

public class colorChangePlayLevels : MonoBehaviour
{
    //PlayLevels Scene - page választásnál más háttérszinek

    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SwipeController swipeController;
    [SerializeField] private Color page1 = new Color(255f / 255f, 255f / 255f, 255f / 255f);
    [SerializeField] private Color page2 = new Color(204f / 255f, 162f / 255f, 250f / 255f);
    [SerializeField] private Color page3 = new Color(255f / 255f, 114f / 255f, 126f / 255f);
    [SerializeField] private Color page4 = new Color(255f / 255f, 100f / 255f, 163f / 255f);
    [SerializeField] private Color page5 = new Color(253f / 255f, 56f / 255f, 56f / 255f);


    void Start()
    {

    }


    void Update()
    {
        if (swipeController.currentPage == 1)
        {
            background.color = page1;
        }
        else if (swipeController.currentPage == 2)
        {
            background.color = page2;
        }
        else if (swipeController.currentPage == 3)
        {
            background.color = page3;
        }
        else if (swipeController.currentPage == 4)
        {
            background.color = page4;
        }
        else if (swipeController.currentPage == 5)
        {
            background.color = page5;
        }

    }

}