using UnityEngine;

public class backgroundSpawn : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;

    private void Start()
    {

        SpawnPrefab();
    }
    void SpawnPrefab()
    {
        int randomIndex = Random.Range(0, prefabsToSpawn.Length);
        GameObject randomPrefab = prefabsToSpawn[randomIndex];

        Instantiate(randomPrefab, transform.position, Quaternion.identity);
    }
}