using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace CustomNamespace
{
    public class DotAbility : AbilityChangeCursor
    {
        [Header("Main")]
        public float dotSize;
        public float damage; //not necessary
        public Sprite dotCursorSprite;

        [Header("Auto Click")] 
        public bool autoClick;
        public float autoClickTimeThreshold;
        public float autoClickFrequency;
        private float holdTimer = 0;

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

            Collider2D[] clickedEnemies = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), dotSize);
            foreach (var enemyCollider2D in clickedEnemies)
            {
                enemyCollider2D.GetComponent<Enemy>().Dead(); //Instant Kill, to be modified
            }
            
            AudioManager.instance.PlaySound(soundFX);
        }

        public override void OnKeyTriggerHolding()
        {
            base.OnKeyTriggerHolding();
            
            holdTimer += Time.deltaTime;

            if (holdTimer >= autoClickTimeThreshold)
            {
                holdTimer -= autoClickFrequency;
                if (autoClick) OnKeyTriggerPressed();
            }
        }

        public override void OnKeyTriggerReleased()
        {
            base.OnKeyTriggerReleased();
            holdTimer = 0;
        }
    }
}
