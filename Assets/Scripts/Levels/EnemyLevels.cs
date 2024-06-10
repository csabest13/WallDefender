using UnityEngine;

public class EnemyLevels : MonoBehaviour
{
    //Fal felé repülõ objektumok - Levels

    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    public int damage;
    [SerializeField] Vector2 minDirection;
    [SerializeField] Vector2 maxDirection;
    [SerializeField] bool fixAngle;

    private Rigidbody2D rb;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Vector2.Lerp(minDirection, maxDirection, Random.value);

        currentSpeed = Random.Range(minSpeed, maxSpeed);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        healthObjectLevels HealthObjectLevels = other.gameObject.GetComponent<healthObjectLevels>();
        if (HealthObjectLevels != null)
        {
            int actualDamage = Mathf.Min(damage, HealthObjectLevels.currentHP);
            HealthObjectLevels.currentHP -= actualDamage;
            Destroy(gameObject);

            if (HealthObjectLevels.currentHP <= 0)
            {
                HealthObjectLevels.currentHP = 0;
                Destroy(gameObject);
            }
        }
    }
}