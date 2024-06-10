using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudSpawnerStart : MonoBehaviour
{
    //egy list�b�l spawnolok random felh�t Startban

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