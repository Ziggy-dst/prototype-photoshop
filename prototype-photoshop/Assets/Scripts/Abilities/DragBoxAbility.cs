using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace CustomNamespace
{
    public class DragBoxAbility : AbilityBase
    {
        public float diagonalLength;
        public float damage; //not necessary
        public GameObject selectionBoxPrefab;

        [Header("Feedbacks")]
        public AudioClip soundFX;


        public override void OnKeyModifierPressed()
        {
            base.OnKeyModifierPressed();
            //TODO: Change Cursor
        }

        public override void OnKeyModifierReleased()
        {
            base.OnKeyModifierReleased();
            //TODO: Change Cursor Back
        }

        public override void OnKeyTriggerPressed()
        {
            base.OnKeyTriggerPressed();
            
            
            // AudioManager.instance.PlaySound(soundFX);
        }

        public override void OnKeyTriggerHolding()
        {
            base.OnKeyTriggerHolding();
            
            
        }

        public override void OnKeyTriggerReleased()
        {
            base.OnKeyTriggerReleased();

        }
    }
}
