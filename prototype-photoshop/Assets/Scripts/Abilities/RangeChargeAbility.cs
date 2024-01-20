using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Managers;
using UnityEngine;

namespace Abilities
{
    public class RangeChargeAbility : AbilityChangeCursor
    {
        private Vector2 origin;
        private GameObject chargeCircleInstance;

        [Header("Main")]
        public float chargeSpeed;
        public GameObject chargeCirclePrefab;

        [Header("Feedbacks")]
        public AudioClip soundFX;
        
        
        protected override void OnKeyModifierPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierPressed(abilityName);
        }

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierReleased(abilityName);

            CursorManager.instance.ShowCursor();
            RemoveCircle();
        }

        protected override void OnKeyTriggerPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyTriggerPressed(abilityName);
            
            CursorManager.instance.HideCursor();
            
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            chargeCircleInstance = Instantiate(chargeCirclePrefab, origin, Quaternion.Euler(Vector3.zero));
            chargeCircleInstance.transform.localScale = Vector2.zero;
            
            // AudioManager.instance.PlaySound(soundFX);
        }

        protected override void OnKeyTriggerHolding(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyTriggerHolding(abilityName);

            chargeCircleInstance.transform.localScale += Vector3.one * Time.deltaTime * chargeSpeed;
        }

        protected override void OnKeyTriggerReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyTriggerReleased(abilityName);

            List<Collider2D> selectedEnemies = new List<Collider2D>();
            chargeCircleInstance.GetComponent<CircleCollider2D>().GetContacts(selectedEnemies);
            foreach (var enemy in selectedEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }

            CursorManager.instance.ShowCursor();
            AudioManager.instance.PlaySound(soundFX);
            RemoveCircle();
        }

        protected override void OnKeyModifierSwitched(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            CursorManager.instance.ShowCursor();
            RemoveCircle();
        }

        private void RemoveCircle()
        {
            Destroy(chargeCircleInstance);
        }
    }
}
