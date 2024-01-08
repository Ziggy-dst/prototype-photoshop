using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public KeySequenceAsset keySequenceAsset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        foreach (var item in keySequenceAsset.keySequenceBindings)
        {
            if (Input.GetKey(item.keyModifier))
            {
                if (Input.GetKeyDown(item.keyTrigger))
                {
                    item.ability.UseAbility();
                }
            }
        }
    }
}
