using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static Action<AbilityNames> OnKeyModifierPressed;
    public static Action<AbilityNames> OnKeyModifierHolding;
    public static Action<AbilityNames> OnKeyModifierReleased;
    public static Action<AbilityNames> OnKeyTriggerPressed;
    public static Action<AbilityNames> OnKeyTriggerHolding;
    public static Action<AbilityNames> OnKeyTriggerReleased;

    public static Action<AbilityNames> OnKeyModifierSwitched;

    public KeySequenceAsset keySequenceAsset;

    private KeyCode _currentPressedKeyModifier = KeyCode.None;
    private int _lastItemIndex = -1;

    private List<KeyCode> _keyModifiers = new List<KeyCode>();
    private List<KeyCode> _keyTriggers = new List<KeyCode>();
    // private List<AbilityBase> _abilities = new List<AbilityBase>();
    private List<AbilityNames> _abilityNames = new List<AbilityNames>();

    private List<KeyCode> _keySequence = new List<KeyCode>(2);

    private void Start()
    {
        StructureAsset();
    }

    void Update()
    {
        HandleKeyMouseInput();
    }

    private void StructureAsset()
    {
        foreach (var item in keySequenceAsset.keySequenceBindings)
        {
            _keyModifiers.Add(item.keyModifier);
            _keyTriggers.Add(item.keyTrigger);
            // _abilities.Add(item.ability);
            _abilityNames.Add(item.abilityName);
        }
    }

    private void HandleKeyMouseInput()
    {
        // check if the pressed key is functional
        if (_keyModifiers.Contains(_currentPressedKeyModifier))
        {
            int itemIndex = _keyModifiers.IndexOf(_currentPressedKeyModifier);

            // check if the pressed key has changed
            if (_lastItemIndex >= 0)
            {
                if (itemIndex != _lastItemIndex)
                {
                    _keySequence.Clear();
                    OnKeyModifierSwitched(_abilityNames[_lastItemIndex]);
                }
            }

            _lastItemIndex = itemIndex;

            var item = keySequenceAsset.keySequenceBindings[itemIndex];

            // check the press order
            if (Input.GetKeyDown(item.keyModifier))
            {
                if (!_keySequence.Contains(item.keyModifier))
                {
                    _keySequence.Add(item.keyModifier);
                    OnKeyModifierPressed(item.abilityName);
                }
            }
            if (Input.GetKeyDown(item.keyTrigger))
            {
                if (!_keySequence.Contains(item.keyTrigger))
                {
                    _keySequence.Add(item.keyTrigger);
                }
            }

            if (Input.GetKey(item.keyModifier))
            {
                OnKeyModifierHolding(item.abilityName);
            }

            // only trigger the function when the modifier is pressed before the trigger
            if (_keySequence.IndexOf(item.keyModifier) < _keySequence.IndexOf(item.keyTrigger))
            {
                if (Input.GetKeyDown(item.keyTrigger)) OnKeyTriggerPressed(item.abilityName);
                // if (Input.GetKeyDown(item.keyTrigger)) item.ability.OnKeyTriggerPressed();

                if (Input.GetKey(item.keyTrigger)) OnKeyTriggerHolding(item.abilityName);
                // if (Input.GetKey(item.keyTrigger)) item.ability.OnKeyTriggerHolding();

                if (Input.GetKeyUp(item.keyTrigger)) OnKeyTriggerReleased(item.abilityName);
                // if (Input.GetKeyUp(item.keyTrigger)) item.ability.OnKeyTriggerReleased();
            }

            if (Input.GetKeyUp(item.keyTrigger))
            {
                _keySequence.Remove(item.keyTrigger);
            }
        }
    }

    /// <summary>
    /// get the newly pressed keys
    /// </summary>
    void OnGUI()
    {
        if (Event.current.isKey)
        {
            if (Event.current.type == EventType.KeyDown)
            {
                if (_keyModifiers.Contains(Event.current.keyCode))
                {
                    _currentPressedKeyModifier = Event.current.keyCode;
                    // Debug.Log(_currentPressedKeyModifier);
                }
            }
            else if (Event.current.type == EventType.KeyUp)
            {
                if (Event.current.keyCode == _currentPressedKeyModifier)
                {
                    int index = _keyModifiers.IndexOf(_currentPressedKeyModifier);
                    OnKeyModifierReleased(_abilityNames[index]);
                    // _abilities[index].OnKeyModifierReleased();
                    _keySequence.Remove(_keyModifiers[index]);
                    _keySequence.Remove(_keyTriggers[index]);
                    // Debug.Log(_currentPressedKeyModifier);
                    _currentPressedKeyModifier = KeyCode.None;
                }
            }
        }
    }
}
