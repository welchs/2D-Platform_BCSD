using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;


    [SerializeField] GameObject monsterPrefab;

    [SerializeField] int maxSpawneCount = 10;
    [SerializeField] float spawnInterval = 1.5f;

    private int currentSpawnCount = 0;

    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (currentSpawnCount < maxSpawneCount)
        {
            SpawnMonster();
            currentSpawnCount++;
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void SpawnMonster()
    {
        Vector2 spawnPos = GetRandomPosition();
        Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
    }

    Vector2 GetRandomPosition()
    {
        return Random.value < 0.5f ? (Vector2)pointA.position : (Vector2)pointB.position;
    }

    

}
