using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public KeySequenceAsset keySequenceAsset;

    private KeyCode _currentPressedKeyCode = KeyCode.None;
    private int _lastItemIndex = -1;

    private List<KeyCode> _keyModifiers = new List<KeyCode>();
    private List<KeyCode> _keyTriggers = new List<KeyCode>();
    private List<AbilityBase> _abilities = new List<AbilityBase>();

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
            _abilities.Add(item.ability);
        }
    }

    private void HandleKeyMouseInput()
    {
        // check if the pressed key is functional
        if (_keyModifiers.Contains(_currentPressedKeyCode))
        {
            int itemIndex = _keyModifiers.IndexOf(_currentPressedKeyCode);

            // check if the pressed key has changed
            if (_lastItemIndex >= 0)
                if (itemIndex != _lastItemIndex) _keySequence.Clear();

            _lastItemIndex = itemIndex;

            var item = keySequenceAsset.keySequenceBindings[itemIndex];

            // check the press order
            if (Input.GetKeyDown(item.keyModifier))
            {
                if (!_keySequence.Contains(item.keyModifier))
                {
                    _keySequence.Add(item.keyModifier);
                    item.ability.OnKeyModifierPressed();
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
                item.ability.OnKeyModifierHolding();
            }

            // only trigger the function when the modifier is pressed before the trigger
            if (_keySequence.IndexOf(item.keyModifier) < _keySequence.IndexOf(item.keyTrigger))
            {
                if (Input.GetKeyDown(item.keyTrigger)) item.ability.OnKeyTriggerPressed();

                if (Input.GetKey(item.keyTrigger)) item.ability.OnKeyTriggerHolding();

                if (Input.GetKeyUp(item.keyTrigger)) item.ability.OnKeyTriggerReleased();
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
                if (Event.current.keyCode != KeyCode.None)
                {
                    _currentPressedKeyCode = Event.current.keyCode;
                    // Debug.Log(_currentPressedKeyCode);
                }
            }
            else if (Event.current.type == EventType.KeyUp)
            {
                if (Event.current.keyCode == _currentPressedKeyCode)
                {
                    if (_keyModifiers.Contains(_currentPressedKeyCode))
                    {
                        int index = _keyModifiers.IndexOf(_currentPressedKeyCode);
                        _abilities[index].OnKeyModifierReleased();
                    }
                    // Debug.Log(_currentPressedKeyCode);
                    _currentPressedKeyCode = KeyCode.None;
                }
            }
        }
    }
}
