using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities
{
    public class AbilityBase : MonoBehaviour
    {
        [Header("Ability Icon")]
        public Sprite iconNormal;
        public Sprite iconPressed;
        public Image iconUI;

        public virtual void Awake()
        {
            iconUI.sprite = iconNormal;
        }

        public virtual void OnKeyModifierPressed()
        {
            iconUI.sprite = iconPressed;
        }

        public virtual void OnKeyModifierHolding()
        {

        }

        public virtual void OnKeyModifierReleased()
        {
            iconUI.sprite = iconNormal;
        }

        public virtual void OnKeyTriggerPressed()
        {

        }

        public virtual void OnKeyTriggerHolding()
        {

        }

        public virtual void OnKeyTriggerReleased()
        {

        }
    }
}

