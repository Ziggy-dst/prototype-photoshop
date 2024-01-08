using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityTest : AbilityBase
    {
        public override void UseAbility()
        {
            base.UseAbility();
            print("test ability");
        }
    }
}
