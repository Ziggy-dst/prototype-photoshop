using System.Collections;
using System.Collections.Generic;
using CustomNamespace;
using UnityEngine;

namespace Abilities
{
    public class AbilityChangeCursor : AbilityBase
    {
        [Header("Cursor")] 
        public Sprite cursor;
        
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

