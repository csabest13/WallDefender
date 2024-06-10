using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    // Tömbbõl spawnoljuk az enemy objecteket
    public GameObject[] prefabsToSpawn;
    [SerializeField] float spawnInterval = 3f; //spawn gyakoriság
    [SerializeField] float initialDelay = 0f; // spawn késleltetés
    [SerializeField] float deathTimer = 3000f; // spawner idõtartalma

    private float timer = 0f;
    private float lifetime = 0f;
    private bool initialSpawnDone = false;

    private void Start()
    {
        timer = -initialDelay;
        initialSpawnDone = false;

        if (initialDelay <= 0f)
        {
            SpawnPrefab();
            initialSpawnDone = true;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        lifetime += Time.deltaTime;

        if (!initialSpawnDone && timer >= 0f)
        {
            SpawnPrefab();
            initialSpawnDone = true;
            timer = 0f;
        }
        else if (initialSpawnDone && timer >= spawnInterval)
        {
            SpawnPrefab();
            timer = 0f;
        }

        if (lifetime >= deathTimer)
        {
            Destroy(gameObject);
        }
    }

    void SpawnPrefab()
    {
        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject randomPrefab = prefabsToSpawn[randomIndex];

        Instantiate(randomPrefab, transform.position, Quaternion.identity);
    }
}