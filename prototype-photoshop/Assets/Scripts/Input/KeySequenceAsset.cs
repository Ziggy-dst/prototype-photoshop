using System;using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "KeySequenceAsset", menuName = "KeyBindings/KeySequenceAsset")]
public class KeySequenceAsset : ScriptableObject
{
    [TableList(ShowIndexLabels = true)]
    public List<ItemHolder> keySequenceBindings = new List<ItemHolder>()
    {
        new ItemHolder()
    };
}

[Serializable]
public class ItemHolder
{
    public KeyCode keyModifier;
    public KeyCode keyTrigger;

    // public AbilityBase ability;
    public AbilityNames abilityName;
}

