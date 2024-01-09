using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class AbilityTest : AbilityBase
    {
        public override void OnKeyPressed()
        {
            print("pressed");
        }

        public override void OnKeyHolding()
        {
            print("test holding");
        }

        public override void OnKeyReleased()
        {
            print("test released");
        }
    }
}
