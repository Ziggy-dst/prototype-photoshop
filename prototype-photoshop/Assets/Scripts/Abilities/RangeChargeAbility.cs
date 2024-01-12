using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace CustomNamespace
{
    public class RangeChargeAbility : AbilityChangeCursor
    {
        private Vector2 origin;
        private GameObject chargeCircleInstance;

        [Header("Main")]
        public float chargeSpeed;
        public float damage; //not necessary
        public GameObject chargeCirclePrefab;

        [Header("Feedbacks")]
        public AudioClip soundFX;
        
        
        public override void OnKeyModifierPressed()
        {
            base.OnKeyModifierPressed();
        }

        public override void OnKeyModifierReleased()
        {
            base.OnKeyModifierReleased();
        }

        public override void OnKeyTriggerPressed()
        {
            base.OnKeyTriggerPressed();
            
            CursorManager.instance.HideCursor();
            
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            chargeCircleInstance = Instantiate(chargeCirclePrefab, origin, Quaternion.Euler(Vector3.zero));
            chargeCircleInstance.transform.localScale = Vector2.zero;
            
            // AudioManager.instance.PlaySound(soundFX);
        }

        public override void OnKeyTriggerHolding()
        {
            base.OnKeyTriggerHolding();

            chargeCircleInstance.transform.localScale += Vector3.one * Time.deltaTime;
        }

        public override void OnKeyTriggerReleased()
        {
            base.OnKeyTriggerReleased();
            
            CursorManager.instance.ShowCursor();
            
            List<Collider2D> selectedEnemies = new List<Collider2D>();
            chargeCircleInstance.GetComponent<CircleCollider2D>().GetContacts(selectedEnemies);
            foreach (var enemy in selectedEnemies)
            {
                enemy.GetComponent<Enemy>().Dead();
            }
            
            Destroy(chargeCircleInstance);
        }
    }
}
