using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityBase : MonoBehaviour
    {
        public InputHandler.TriggerCondition triggerCondition = 0;

        public virtual void UseAbility()
        {

        }
    }
}

