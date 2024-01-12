using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityBase : MonoBehaviour
    {
        public AbilityNames abilityName;

        private void OnEnable()
        {
            InputHandler.OnKeyModifierPressed += OnKeyModifierPressed;
            InputHandler.OnKeyModifierHolding += OnKeyModifierHolding;
            InputHandler.OnKeyModifierReleased += OnKeyModifierReleased;

            InputHandler.OnKeyTriggerPressed += OnKeyTriggerPressed;
            InputHandler.OnKeyTriggerHolding += OnKeyTriggerHolding;
            InputHandler.OnKeyTriggerReleased += OnKeyTriggerReleased;

            InputHandler.OnKeyModifierSwitched += OnKeyModifierSwitched;
        }

        private void OnDisable()
        {
            InputHandler.OnKeyModifierPressed -= OnKeyModifierPressed;
            InputHandler.OnKeyModifierHolding -= OnKeyModifierHolding;
            InputHandler.OnKeyModifierReleased -= OnKeyModifierReleased;

            InputHandler.OnKeyTriggerPressed -= OnKeyTriggerPressed;
            InputHandler.OnKeyTriggerHolding -= OnKeyTriggerHolding;
            InputHandler.OnKeyTriggerReleased -= OnKeyTriggerReleased;

            InputHandler.OnKeyModifierSwitched -= OnKeyModifierSwitched;
        }

        protected virtual void OnKeyModifierPressed(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyModifierHolding(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyModifierReleased(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyTriggerPressed(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyTriggerHolding(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyTriggerReleased(AbilityNames abilityName)
        {

        }

        protected virtual void OnKeyModifierSwitched(AbilityNames abilityName)
        {

        }
    }
}

