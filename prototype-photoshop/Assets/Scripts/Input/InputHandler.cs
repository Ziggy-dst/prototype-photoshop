using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public enum TriggerCondition
    {
        Down = 0,
        Hold = 1,
        Up = 2
    }

    public KeySequenceAsset keySequenceAsset;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        foreach (var item in keySequenceAsset.keySequenceBindings)
        {
            if (Input.GetKey(item.keyModifier))
            {
                if (SatisfyTriggerCondition(item.keyTrigger, item.ability.triggerCondition))
                {
                    item.ability.UseAbility();
                }
            }
        }
    }

    private bool SatisfyTriggerCondition(KeyCode keyTrigger, TriggerCondition triggerCondition)
    {
        switch (triggerCondition)
        {
            case TriggerCondition.Down:
                if (Input.GetKeyDown(keyTrigger)) return true;
                break;
            case TriggerCondition.Hold:
                if (Input.GetKey(keyTrigger)) return true;
                break;
            case TriggerCondition.Up:
                if (Input.GetKeyUp(keyTrigger)) return true;
                break;
        }

        return false;
    }
}
