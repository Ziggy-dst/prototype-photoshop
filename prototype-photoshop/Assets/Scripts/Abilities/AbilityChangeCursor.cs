using System.Collections;
using System.Collections.Generic;
using CustomNamespace;
using Managers;
using UnityEngine;

namespace Abilities
{
    public class AbilityChangeCursor : AbilityBase
    {
        [Header("Cursor")] 
        public Sprite cursor;

        [Header("Combat")] 
        public float damage;
        
        protected override void OnKeyModifierPressed(AbilityNames abilityName)
        {
            base.OnKeyModifierPressed(abilityName);
            CursorManager.instance.ChangeCursor(cursor);
        }

        protected override void OnKeyModifierReleased(AbilityNames abilityName)
        {
            base.OnKeyModifierReleased(abilityName);
            CursorManager.instance.ResumeCursor();
        }
    }
}

