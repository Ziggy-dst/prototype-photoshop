using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Prefabs")] 
    public GameObject enemyGroup;

    [Header("Auto Spawn")]
    public bool autoSpawn = false;
    public int autoSpawnEnemyQuantityThreshold;

    [Header("Spawn")] 
    public float screenSizeX;
    public float screenSizeY;
    public float maxDistanceToScreenEdge;
    public float screenDistanceOffset;


    [SerializeField] private int currentEnemyQuantity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyQuantity();
        
        if(autoSpawn && currentEnemyQuantity < autoSpawnEnemyQuantityThreshold) Spawn();

        if (Input.GetMouseButtonDown(1))
        {
            Spawn();
        }
    }

    public void UpdateEnemyQuantity()
    {
        currentEnemyQuantity = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).Length;
    }

    public void Spawn()
    {
        for (int i = 0; i < 1;)
        {
            Vector2 spawnPos =
                new Vector2(Random.Range(-screenSizeX - maxDistanceToScreenEdge, screenSizeX + maxDistanceToScreenEdge),
                    Random.Range(-screenSizeY - maxDistanceToScreenEdge, screenSizeY + maxDistanceToScreenEdge));
            
            if (Mathf.Abs(spawnPos.x) > screenSizeX + screenDistanceOffset || Mathf.Abs(spawnPos.y) > screenSizeY + screenDistanceOffset)
            {
                Instantiate(enemyGroup, spawnPos, Quaternion.Euler(Vector3.zero));
                i++;
            }
        }
    }
}
