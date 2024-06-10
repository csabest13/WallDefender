using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHighScore : MonoBehaviour
{
    //Fal felé repülõ objektumok - HighScore

    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] int damage;
    [SerializeField] Vector2 minDirection;
    [SerializeField] Vector2 maxDirection;
    [SerializeField] bool fixAngle;

    private Rigidbody2D rb;
    private float currentSpeed;
    private float timeSinceLastChange;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Vector2.Lerp(minDirection, maxDirection, Random.value);

        currentSpeed = Random.Range(minSpeed, maxSpeed);

        timeSinceLastChange = 0f;

        rb.AddForce(randomDirection.normalized * currentSpeed, ForceMode2D.Force);

        if (fixAngle)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, randomDirection);
        }
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= 1f)
        {
            currentSpeed = Random.Range(minSpeed, maxSpeed);

            timeSinceLastChange = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        healthObjectHighScore HealthObjectHighScore = other.gameObject.GetComponent<healthObjectHighScore>();
        if (HealthObjectHighScore != null)
        {
            // Fontos, hogy ne legyen minuszos a currentHP a legvégén
            int actualDamage = Mathf.Min(damage, HealthObjectHighScore.currentHP);
            HealthObjectHighScore.currentHP -= actualDamage;
            Destroy(gameObject);

            //enélkül bebuggolt a játék
            if (HealthObjectHighScore.currentHP <= 0)
            {
                HealthObjectHighScore.currentHP = 0;
                Destroy(gameObject);
            }
        }
    }
}