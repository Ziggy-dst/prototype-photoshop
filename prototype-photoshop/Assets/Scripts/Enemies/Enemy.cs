using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hitPoint = 1;
    public GameObject damagePopupPrefab;
    public bool feedbacks = true;
    protected SpriteRenderer _spriteRenderer;
    private Color originalColor;
    [Header("Death Feedbacks")] 
    public GameObject deathParticlePrefab;
    
    public virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color;
    }
    
    public virtual void Update()
    {
        if (hitPoint <= 0) 
        {
            Dead();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        hitPoint -= damage;
        PopupDamage(damage);
        if (feedbacks)
        {
            _spriteRenderer.DOColor(Color.white, 0);
            _spriteRenderer.DOColor(originalColor, 0.25f);
        }
    }

    public virtual void Dead()
    {
        GameObject deathParticle = Instantiate(deathParticlePrefab, transform.position, Quaternion.Euler(Vector3.zero));
        // deathParticle.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }

    public virtual void PopupDamage(float damage)
    {
        GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.Euler(Vector3.zero));
        TextMeshPro damageText = damagePopup.GetComponent<TextMeshPro>();
        Sequence popupSequence = DOTween.Sequence();

        damageText.text = damage.ToString();
        
        popupSequence
            .Append(damageText.transform.DOScale(Vector3.zero, 0))
            .Append(damageText.transform.DOScale(Vector3.one, 0.5f))
            .Insert(0, damageText.transform.DOMoveY(transform.position.y + 1, 2f))
            .Insert(1, damageText.DOFade(0, 1f))
            .OnComplete((() => { Destroy(damagePopup); }));
        popupSequence.Play();
    }
}
