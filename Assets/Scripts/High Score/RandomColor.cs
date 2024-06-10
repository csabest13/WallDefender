using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    //HighScore random háttérszin

    private void Start()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        GetComponent<Renderer>().material.color = randomColor;
    }
}
