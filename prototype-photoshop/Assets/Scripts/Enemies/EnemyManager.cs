using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Prefabs")] 
    public GameObject groupEnemy;
    public float groupWeight;
    public GameObject inflateEnemy;
    public float inflateWeight;
    public GameObject lineEnemy;
    public float lineWeight;

    [Header("Auto Spawn")]
    public bool autoSpawn = false;
    public int autoSpawnEnemyQuantityThreshold;

    [Header("Spawn")] 
    public float screenSizeX;
    public float screenSizeY;
    public float maxDistanceToScreenEdge;
    public float screenDistanceOffset;
    
    private List<(Action, float)> functionsWithWeights = new List<(Action, float)>();


    [SerializeField] private int currentEnemyQuantity;
    
    void Start()
    {
        functionsWithWeights.Add((()=>SpawnOffScreen(groupEnemy), groupWeight)); 
        functionsWithWeights.Add((()=>SpawnOnScreen(inflateEnemy), inflateWeight));  
        functionsWithWeights.Add((()=>SpawnOffScreen(lineEnemy), lineWeight)); 
    }
    
    void Update()
    {
        UpdateEnemyQuantity();

        if (autoSpawn && currentEnemyQuantity < autoSpawnEnemyQuantityThreshold) SpawnRandomEnemyByWeight();

        if (Input.GetMouseButtonDown(1))
        {
            SpawnOffScreen(groupEnemy);
            SpawnOnScreen(inflateEnemy);
        }
    }

    public void UpdateEnemyQuantity()
    {
        currentEnemyQuantity = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
    }

    public void SpawnOffScreen(GameObject enemyPrefab)
    {
        for (int i = 0; i < 1;)
        {
            Vector2 spawnPos =
                new Vector2(UnityEngine.Random.Range(-screenSizeX - maxDistanceToScreenEdge, screenSizeX + maxDistanceToScreenEdge),
                    UnityEngine.Random.Range(-screenSizeY - maxDistanceToScreenEdge, screenSizeY + maxDistanceToScreenEdge));
            
            if (Mathf.Abs(spawnPos.x) > screenSizeX + screenDistanceOffset || Mathf.Abs(spawnPos.y) > screenSizeY + screenDistanceOffset)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(Vector3.zero));
                i++;
            }
        }
    }

    public void SpawnOnScreen(GameObject enemyPrefab)
    {
        Vector2 spawnPos =
            new Vector2(UnityEngine.Random.Range(-screenSizeX, screenSizeX), UnityEngine.Random.Range(-screenSizeY, screenSizeY));

        Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(Vector3.zero));
    }
    
    void SpawnRandomEnemyByWeight()
    {
        float totalWeight = 0f;
        foreach (var (func, weight) in functionsWithWeights)
        {
            totalWeight += weight;
        }

        float randomPoint = UnityEngine.Random.value * totalWeight;

        foreach (var (func, weight) in functionsWithWeights)
        {
            if (randomPoint < weight)
            {
                func();
                return;
            }
            randomPoint -= weight;
        }
    }
}
