using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hitPoint = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}
