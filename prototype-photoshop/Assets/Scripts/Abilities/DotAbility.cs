using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace CustomNamespace
{
    public class DotAbility : AbilityChangeCursor
    {
        public float dotSize;
        public float damage; //not necessary
        public Sprite dotCursorPressedSprite;

        [Header("Auto Click")] 
        public bool autoClick;
        public float autoClickTimeThreshold;
        public float autoClickFrequency;
        private float holdTimer = 0;

        [Header("Feedbacks")]
        public AudioClip soundFX;

        protected override void OnKeyModifierPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierPressed(abilityName);
            
            // reset
            holdTimer = 0;
        }

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyModifierReleased(abilityName);
        }

        protected override void OnKeyTriggerPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            Collider2D[] clickedEnemies = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), dotSize);
            foreach (var enemyCollider2D in clickedEnemies)
            {
                if(enemyCollider2D.TryGetComponent<Enemy>(out Enemy enemy)) enemy.Dead(); //Instant Kill, to be modified
            }

            AudioManager.instance.PlaySound(soundFX);
            CursorManager.instance.ChangeCursor(dotCursorPressedSprite);
        }

        protected override void OnKeyTriggerHolding(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            
            holdTimer += Time.deltaTime;

            if (holdTimer >= autoClickTimeThreshold)
            {
                holdTimer -= autoClickFrequency;
                if (autoClick) OnKeyTriggerPressed(abilityName);
            }
        }

        protected override void OnKeyTriggerReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            base.OnKeyTriggerReleased(abilityName);
            // reset
            holdTimer = 0;
            CursorManager.instance.ChangeCursor(cursor);
        }
    }
}
