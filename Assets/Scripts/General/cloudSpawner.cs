using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawner : MonoBehaviour
{
    //felhõ spawn-hoz használt script

    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float minYposition = 0f;
    [SerializeField] float maxYposition = 5f;

    private float currentSpeed;
    private float direction = 1f;
    private float timeSinceLastSpeedChange = 0f;

    void Start()
    {
        SetRandomSpeed();
    }

    void Update()
    {
        timeSinceLastSpeedChange += Time.deltaTime;

        if (timeSinceLastSpeedChange >= 1f)
        {
            SetRandomSpeed();
            timeSinceLastSpeedChange = 0f;
        }

        MoveObject();
    }

    private void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void MoveObject()
    {
        transform.position += Vector3.up * currentSpeed * direction * Time.deltaTime;

        if (transform.position.y >= maxYposition)
        {
            transform.position = new Vector3(transform.position.x, maxYposition, transform.position.z);
            direction = -1f;
        }
        else if (transform.position.y <= minYposition)
        {
            transform.position = new Vector3(transform.position.x, minYposition, transform.position.z);
            direction = 1f;
        }
    }
}