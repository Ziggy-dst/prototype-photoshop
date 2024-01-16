using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Managers;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Basic Setup")]
    public SpriteRenderer spriteRenderer;
    
    [Header("Health")]
    public float initialHealthPoint;
    public float healthPoint;
    
    
    [Header("Battle")]
    public bool immuneToDamage;
    public float getHitCD;
    private float getHitCDTimer;

    [Header("Respawn")]
    public bool respawnOnDeath;
    public float respawnCD;

    [Header("Feedback")] 
    public bool feedback;
    
    public AudioClip feedbackAudio;

    public Image screenFlashMask;

    private GameObject spawnPoint;

    private bool isDead;


    void Awake()
    {
        healthPoint = initialHealthPoint;
        immuneToDamage = false;
        getHitCDTimer = 0;
        // spawnPoint = GameObject.FindWithTag("Spawn Point");
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (healthPoint <= 0)
        {
            if (!isDead)
            {
                Die();
            }
        }

        if (getHitCDTimer <= 0)
        {
            getHitCDTimer = 0;
            immuneToDamage = false;
        }
        else
        {
            getHitCDTimer -= Time.deltaTime;
            immuneToDamage = true;
        }
    }

    void Die()
    {
        isDead = true;
        healthPoint = 0;
        immuneToDamage = true;
        
        //Dead Function
        
        if (respawnOnDeath) StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnCD);
        healthPoint = initialHealthPoint;
        immuneToDamage = false;
        // transform.position = spawnPoint.transform.position;
    }

    // void Respawn()
    // {
    //     healthPoint = initialHealthPoint;
    //     immuneToDamage = false;
    //     // transform.position = spawnPoint.transform.position;
    // }

    public void TakeDamage(float dmg)
    {
        if (!immuneToDamage)
        {
            healthPoint -= dmg;
            immuneToDamage = true;
            spriteRenderer.DOColor(Color.red, getHitCD).SetEase(Ease.Flash, 16, 1);
            if (feedback) AudioManager.instance.PlaySound(feedbackAudio);
            if(feedback) screenFlashMask.DOColor(Color.red, getHitCD).SetEase(Ease.Flash, 16, 1);
            getHitCDTimer = getHitCD;
            
            print(gameObject.name + "health: " + healthPoint);
        }
    }
}