using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private Controls Controls;
    public event Action<bool> onPrimaryFire;
    public event Action<Vector2> onMove;

    private void OnEnable()
    {
        if (Controls == null)
        {
            Controls = new Controls();
            Controls.Player.SetCallbacks(this);
        }

        Controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        onMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            onPrimaryFire?.Invoke(true);
        }
        else if (context.canceled)
        {
            onPrimaryFire?.Invoke(false);
        }
    }
}
