using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InflatingEnemy : Enemy
{
    public float explodeThreshold;

    private SpriteRenderer _spriteRenderer;

    public bool isDead;
    
    void Start()
    {
        transform.localScale = Vector3.zero;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        isDead = false;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (transform.localScale.x >= explodeThreshold) isDead = true;

        if (isDead)
        {
            _spriteRenderer.color = new Color32(154, 69, 41, 255);
            _spriteRenderer.sortingOrder = -1;
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(this);
        }
        else transform.localScale += Vector3.one * Time.deltaTime;
    }

    public override void Dead()
    {
        isDead = true;
    }
}
