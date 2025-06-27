using System;
using System.Collections;
using Runner;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    public Vector2 spawnDelayRange = new Vector2(4f, 8f);
    private void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            var spawnCoolTime = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
            Debug.Log(spawnCoolTime);
            yield return new WaitForSeconds(spawnCoolTime);
            
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
    }
}
