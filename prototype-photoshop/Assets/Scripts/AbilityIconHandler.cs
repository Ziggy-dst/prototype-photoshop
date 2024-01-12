using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace CustomNamespace
{
    public class AbilityIconHandler : AbilityBase
    {
        [Header("Ability Icon")]
        public Sprite iconNormal;
        public Sprite iconPressed;
        
        private Image iconUI;

        private void Awake()
        {
            iconUI = GetComponent<Image>();
            iconUI.sprite = iconNormal;
        }

        protected override void OnKeyModifierPressed(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            iconUI.sprite = iconPressed;
        }

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;
            iconUI.sprite = iconNormal;
        }

        protected override void OnKeyModifierSwitched(AbilityNames abilityName)
        {
            if (!abilityName.Equals(this.abilityName)) return;

            iconUI.sprite = iconNormal;
        }
    }
}
