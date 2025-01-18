using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private ObjectGrabber _grabber;

    private void Start()
    {
        _grabber = GetComponent<ObjectGrabber>();
    }
    
    public void OnMousePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            _grabber.OnMousePressed();
        
        if (ctx.canceled)
            _grabber.OnMouseReleased();
    }
}
