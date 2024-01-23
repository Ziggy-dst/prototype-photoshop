using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class InflatingEnemy : Enemy
{
    public float explodeThreshold;
    
    public override void Start()
    {
        base.Start();
        transform.localScale = Vector3.zero;
    }
    
    public override void Update()
    {
        base.Update();
        
        if (transform.localScale.x >= explodeThreshold)
        {
            GameObject colorCircle = new GameObject("Color Circle");
            SpriteRenderer colorCircleRenderer = colorCircle.AddComponent<SpriteRenderer>();

            colorCircle.transform.position = transform.position;
            colorCircle.transform.localScale = transform.localScale;

            colorCircleRenderer.sprite = _spriteRenderer.sprite;
            colorCircleRenderer.color = new Color32(154, 69, 41, 255);
            colorCircleRenderer.sortingOrder = -1;

            Destroy(gameObject);
        }
        
        else transform.localScale += Vector3.one * Time.deltaTime;
    }
}
