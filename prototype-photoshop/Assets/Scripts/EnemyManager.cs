using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Prefabs")] 
    public GameObject enemyGroup;
    public Vector2 enemyGroupSizeRandomRange;

    [Header("Auto Spawn")]
    public bool autoSpawn = false;
    public int autoSpawnEnemyQuantityThreshold;

    [Header("Spawn")] 
    public float screenSizeX;
    public float screenSizeY;
    public float distanceToScreenEdge;


    [SerializeField] private readonly int currentEnemyQuantity;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        
    }
}
