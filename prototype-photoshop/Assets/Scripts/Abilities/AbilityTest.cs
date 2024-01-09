using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityTest : AbilityBase
    {
        public override void OnKeyTriggerPressed()
        {
            print("pressed");
        }

        public override void OnKeyTriggerHolding()
        {
            print("test holding");
        }

        public override void OnKeyTriggerReleased()
        {
            print("test released");
        }
    }
}
