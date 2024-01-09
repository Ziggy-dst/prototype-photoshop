using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGroupManager : MonoBehaviour
{
    public GameObject follower;
    public Vector2 enemyGroupSizeRandomRange;


    private void Awake()
    {
        for (int i = 0; i < Random.Range(enemyGroupSizeRandomRange.x, enemyGroupSizeRandomRange.y); i++)
        {
            Instantiate(follower, transform);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
