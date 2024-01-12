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
        
        public override void OnKeyModifierPressed()
        {
            base.OnKeyModifierPressed();
            CursorManager.instance.ChangeCursor(cursor);
        }

        public override void OnKeyModifierReleased()
        {
            base.OnKeyModifierReleased();
            CursorManager.instance.ResumeCursor();
        }
        
    }
}

